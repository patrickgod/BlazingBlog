using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.GetArticleByIdForEditing
{
    public class GetArticleByIdForEditingQueryHandler : IQueryHandler<GetArticleByIdForEditingQuery, ArticleResponse?>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public GetArticleByIdForEditingQueryHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleResponse?>> Handle(GetArticleByIdForEditingQuery request, CancellationToken cancellationToken)
        {
            var canEdit = await _userService.CurrentUserCanEditArticleAsync(request.Id);
            if (!canEdit)
            {
                return Result.Fail<ArticleResponse?>("You're not allowed to edit this article.");
            }

            var article = await _articleRepository.GetArticleByIdAsync(request.Id);
            var articleResponse = article.Adapt<ArticleResponse>();

            return articleResponse;
        }
    }
}
