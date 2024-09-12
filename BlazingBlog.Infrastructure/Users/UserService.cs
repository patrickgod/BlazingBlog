using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;
using BlazingBlog.Domain.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlazingBlog.Infrastructure.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IArticleRepository _articleRepository;

        public UserService(UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IArticleRepository articleRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _articleRepository = articleRepository;
        }

        public async Task<bool> CurrentUserCanCreateArticlesAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                return false;
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var isWriter = await _userManager.IsInRoleAsync(user, "Writer");

            var result = isAdmin || isWriter;
            return result;
        }

        public async Task<bool> CurrentUserCanEditArticleAsync(int articleId)
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                return false;
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            var isWriter = await _userManager.IsInRoleAsync(user, "Writer");

            var article = await _articleRepository.GetArticleByIdAsync(articleId);
            if (article is null)
            {
                return false;
            }

            var result = isAdmin || (isWriter && user.Id == article.UserId);
            return result;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user is null)
            {
                throw new UserNotAuthorizedException();
            }

            return user.Id;
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return [];
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            var user = await GetCurrentUserAsync();
            var result = user is not null &&
                await _userManager.IsInRoleAsync(user, role);
            return result;
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null || httpContext.User is null)
            {
                return null;
            }

            var user = await _userManager.GetUserAsync(httpContext.User);
            return user;
        }
    }
}
