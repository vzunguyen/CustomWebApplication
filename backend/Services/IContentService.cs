public interface IContentService
{
    Task<IEnumerable<Content>> GetAllContent();
    Task<Content> GetContentById(int id);
    Task<IEnumerable<Content>> GetContentByUserId(int userId);
    Task<IEnumerable<Content>> GetCurrentUserContent(HttpContext httpContext);
    Task<IResult> CreateContent(ContentDto contentDto, HttpContext httpContext);
    Task<IResult> UpdateContent(int id, ContentDto contentDto,  HttpContext httpContext);
    Task<IResult> DeleteContent(int id, HttpContext httpContext);
}