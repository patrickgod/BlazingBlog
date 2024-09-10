using BlazingBlog.Domain.Users;
using BlazingBlog.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazingBlog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<IUser>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Select(user => (IUser)user).ToListAsync();
        }

        public async Task<IUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
