
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.GetArticlesByCurrentUser
{
    public class GetArticlesByCurrentUserQueryHandler :
        IQueryHandler<GetArticlesByCurrentUserQuery, List<ArticleResponse>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public GetArticlesByCurrentUserQueryHandler(IArticleRepository articleRepository,
                IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<List<ArticleResponse>>> Handle(GetArticlesByCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserIdAsync();
            var articles = await _articleRepository.GetArticlesByUserAsync(userId);
            var response = articles.Adapt<List<ArticleResponse>>();

            return response.OrderByDescending(a => a.DatePublished).ToList();
        }
    }
}
