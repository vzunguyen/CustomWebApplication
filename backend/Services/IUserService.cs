using backend.Dtos;

public interface IUserService
{
    Task<IResult> Register(UserDto userDto);
    Task<IResult> Login(LoginDto loginDto);
    Task<User> GetUserByUsername(string username);
    Task<User> GetUserById(int id);
    Task<User?> GetCurrentUser(HttpContext httpContext);
    Task<IEnumerable<User>> GetAllUsers();
    Task<IResult> SendFriendRequest(int recipientId, HttpContext httpContext);
    Task<IEnumerable<User>> GetSentRequests(HttpContext httpContext);
    Task<IResult> AcceptFriendRequest(int friendId, HttpContext httpContext);
    Task<IResult> RemoveFriend(int friendId, HttpContext httpContext);
    Task<IEnumerable<User>> GetPendingFriendRequests(HttpContext httpContext);

    Task<IEnumerable<User>> GetFriends(HttpContext httpContext);
}