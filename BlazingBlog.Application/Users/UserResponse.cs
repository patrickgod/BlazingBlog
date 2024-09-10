namespace BlazingBlog.Application.Users
{
    public record struct UserResponse(
        string Id,
        string UserName,
        string Email)
    {
    }
}
