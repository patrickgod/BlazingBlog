using BlazingBlog.Application.Exceptions;
using BlazingBlog.Application.Users;

namespace BlazingBlog.Application.Articles.CreateArticle
{
    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, ArticleResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserService _userService;

        public CreateArticleCommandHandler(IArticleRepository articleRepository,
            IUserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
        }

        public async Task<Result<ArticleResponse>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newArticle = request.Adapt<Article>();
                newArticle.UserId = await _userService.GetCurrentUserIdAsync();
                if (!await _userService.CurrentUserCanCreateArticlesAsync())
                {
                    return FailingResult();
                }

                var article = await _articleRepository.CreateArticleAsync(newArticle);
                return article.Adapt<ArticleResponse>();
            }
            catch (UserNotAuthorizedException)
            {
                return FailingResult();
            }
        }

        private Result<ArticleResponse> FailingResult()
        {
            return Result.Fail<ArticleResponse>("You're not allowed to create articles. Sorry! :/");
        }
    }
}
