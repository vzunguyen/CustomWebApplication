using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class ContentService : IContentService
{
    private readonly ApplicationDbContext _context;

    public ContentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Content>> GetAllContent()
    {
        return await _context.Content.ToListAsync();
    }

    public async Task<Content> GetContentById(int id)
    {
        return await _context.Content.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    // get content by userid
    public async Task<IEnumerable<Content>> GetContentByUserId(HttpContext httpContext)
    {
        var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        if (!hasValidUserId)
        {
            return null;
        }
        
        return await _context.Content.Where(c => c.UserId == userId).ToListAsync();
    }
    
    public async Task<IEnumerable<Content>> GetCurrentUserContent(HttpContext httpContext)
    {
        var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        if (!hasValidUserId)
        {
            return null;
        }
        
        return await _context.Content.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<IResult> CreateContent(ContentDto contentDto, HttpContext httpContext)
    {
        var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        if (!hasValidUserId)
        {
            return Results.Unauthorized();
        }
    
        var content = new Content
        {
            Title = contentDto.Title,
            Description = contentDto.Description,
            UserId = userId
        };

        _context.Content.Add(content);
        await _context.SaveChangesAsync();

        return Results.Ok("Content created successfully.");
    }

    public async Task<IResult> UpdateContent(int id, ContentDto contentDto, HttpContext httpContext)
    {
        var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        if (!hasValidUserId)
        {
            return Results.Unauthorized();
        }
        
        var content = await _context.Content.FindAsync(id);
        if (content == null)
        {
            return Results.NotFound();
        }

        content.Title = contentDto.Title;
        content.Description = contentDto.Description;

        await _context.SaveChangesAsync();

        return Results.Ok("Content updated successfully.");
    }

    public async Task<IResult> DeleteContent(int id, HttpContext httpContext)
    {
        var hasValidUserId = int.TryParse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var userId);
        if (!hasValidUserId)
        {
            return Results.Unauthorized();
        }
        
        var content = await _context.Content.FindAsync(id);
        if (content == null)
        {
            return Results.NotFound();
        }

        _context.Content.Remove(content);
        await _context.SaveChangesAsync();

        return Results.Ok("Content deleted successfully.");
    }
}