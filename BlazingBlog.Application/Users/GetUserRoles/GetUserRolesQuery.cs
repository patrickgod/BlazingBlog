namespace BlazingBlog.Application.Users.GetUserRoles
{
    public class GetUserRolesQuery : IQuery<List<string>>
    {
        public required string UserId { get; set; }
    }
}
