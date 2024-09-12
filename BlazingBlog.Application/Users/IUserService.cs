namespace BlazingBlog.Application.Users
{
    public interface IUserService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<bool> IsCurrentUserInRoleAsync(string role);
        Task<bool> CurrentUserCanCreateArticlesAsync();
        Task<bool> CurrentUserCanEditArticleAsync(int articleId);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task AddRoleToUserAsync(string userId, string roleName);
    }
}
