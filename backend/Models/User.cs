
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public List<Content> Content { get; set; }
    public List<UserFriendship> Friends { get; set; }
}