using BlazingBlog.Domain.Users;

namespace BlazingBlog.Application.Users.GetUsers
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<Result<List<UserResponse>>> Handle(GetUserQuery request,
            CancellationToken cancellationToken)
        {
            if (!await _userService.IsCurrentUserInRoleAsync("Admin"))
            {
                return Result.Fail<List<UserResponse>>("You're not allowed to see all users.");
            }

            var users = await _userRepository.GetAllUsersAsync();
            var response = users.Adapt<List<UserResponse>>();

            return response;
        }
    }
}
