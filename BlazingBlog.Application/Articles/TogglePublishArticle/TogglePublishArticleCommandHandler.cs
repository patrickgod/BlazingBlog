
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.TogglePublishArticle
{
    public class TogglePublishArticleCommandHandler
        : ICommandHandler<TogglePublishArticleCommand, ArticleResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public TogglePublishArticleCommandHandler(IArticleRepository articleRepository, IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleResponse>> Handle(TogglePublishArticleCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _userService.CurrentUserCanEditArticleAsync(request.ArticleId))
            {
                return Result.Fail<ArticleResponse>("You're not allowed to edit this article!");
            }

            var articleToUpdate = await _articleRepository.GetArticleByIdAsync(request.ArticleId);
            if (articleToUpdate == null)
            {
                return Result.Fail<ArticleResponse>("Article does not exist.");
            }

            articleToUpdate.IsPublished = !articleToUpdate.IsPublished;
            articleToUpdate.DateUpdated = DateTime.Now;
            if (articleToUpdate.IsPublished)
            {
                articleToUpdate.DatePublished = DateTime.Now;
            }

            var article = await _articleRepository.UpdateArticleAsync(articleToUpdate);
            if (article is null)
                return Result.Fail<ArticleResponse>("Article does not exist");

            return article.Adapt<ArticleResponse>();
        }
    }
}
