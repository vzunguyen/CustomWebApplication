using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IResult> Register(UserDto userDto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);
        if (existingUser != null)
        {
            return Results.BadRequest("Username is already taken.");
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Results.Ok("User registered successfully.");
    }

    public async Task<IResult> Login(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
        {
            return Results.BadRequest("Invalid username or password.");
        }

        var token = GenerateJwtToken(user);
            return Results.Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryInMinutes"]));

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task<IEnumerable<User>> GetAllUsers() => await _context.Users.ToListAsync();
    
    // get user by username
    public async Task<User> GetUserByUsername(string username) => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    
    // get user by userId
    public async Task<User> GetUserById(int userId) => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public async Task<User?> GetCurrentUser(HttpContext httpContext)
    {
        return int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId)
            ? await _context.Users.FindAsync(userId)
            : null;
    }
    
     public async Task<IResult> SendFriendRequest(int recipientId, HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var senderId);
    if (!hasValidUserId)
    {
        return Results.Unauthorized();
    }
    
    var sender = await _context.Users.FindAsync(senderId);
    var recipient = await _context.Users.FindAsync(recipientId);

    if (sender == null || recipient == null)
    {
        return Results.BadRequest("Invalid user IDs.");
    }

    var existingFriendship = await _context.UserFriendships
        .FirstOrDefaultAsync(uf => (uf.UserId == senderId && uf.FriendId == recipientId) ||
                                   (uf.UserId == recipientId && uf.FriendId == senderId));

    if (existingFriendship != null)
    {
        return Results.BadRequest("Friend request already sent or accepted.");
    }

    var friendRequest = new UserFriendship
    {
        User = recipient,
        Friend = sender,
        IsAccepted = false
    };

    _context.UserFriendships.Add(friendRequest);
    await _context.SaveChangesAsync();

    return Results.Ok("Friend request sent successfully.");
}

public async Task<IResult> AcceptFriendRequest(int friendId, HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
    if (!hasValidUserId)
    {
        return Results.Unauthorized();
    }
    
    var friendship = await _context.UserFriendships
        .FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FriendId == friendId && !uf.IsAccepted);

    if (friendship == null)
    {
        return Results.BadRequest("No pending friend request found.");
    }

    friendship.IsAccepted = true;

    var reverseFriendship = new UserFriendship
    {
        UserId = friendId,
        FriendId = userId,
        IsAccepted = true
    };

    _context.UserFriendships.Add(reverseFriendship);
    await _context.SaveChangesAsync();

    return Results.Ok("Friend request accepted successfully.");
}

public async Task<IResult> RemoveFriend(int friendId, HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
    if (!hasValidUserId)
    {
        return Results.Unauthorized();
    }
    
    var friendship = await _context.UserFriendships
        .FirstOrDefaultAsync(uf => (uf.UserId == userId && uf.FriendId == friendId) ||
                                   (uf.UserId == friendId && uf.FriendId == userId));

    if (friendship == null)
    {
        return Results.BadRequest("No friendship found.");
    }

    _context.UserFriendships.Remove(friendship);
    
    var reverseFriendship = new UserFriendship
    {
        UserId = friendId,
        FriendId = userId,
        IsAccepted = true
    };

    _context.UserFriendships.Remove(reverseFriendship);
    
    await _context.SaveChangesAsync();

    return Results.Ok("Friend removed successfully.");
}

public async Task<IEnumerable<User>> GetPendingFriendRequests(HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
    if (!hasValidUserId)
    {
        return new List<User>();
    }

    var pendingRequests = await _context.UserFriendships
        .Where(uf => uf.UserId == userId && !uf.IsAccepted)
        .Select(uf => uf.Friend)
        .ToListAsync();

    return pendingRequests;
}

public async Task<IEnumerable<User>> GetSentRequests(HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
    if (!hasValidUserId)
    {
        return new List<User>();
    }

    var sentRequests = await _context.UserFriendships
        .Where(uf => uf.FriendId == userId && !uf.IsAccepted)
        .Select(uf => uf.User)
        .ToListAsync();

    return sentRequests;
}

public async Task<IEnumerable<User>> GetFriends(HttpContext httpContext)
{
    var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
    if (!hasValidUserId)
    {
        return new List<User>();
    }
    var friends = await _context.UserFriendships
        .Where(uf => uf.UserId == userId && uf.IsAccepted)
        .Select(uf => uf.Friend)
        .ToListAsync();

    return friends;
}
}