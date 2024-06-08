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

        public Task<bool> CurrentUserCanCreateArticlesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CurrentUserCanEditArticleAsync(int articleId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsCurrentUserInRoleAsync(string role)
        {
            throw new NotImplementedException();
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
