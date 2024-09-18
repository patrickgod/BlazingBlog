using BlazingBlog.Application.Articles.GetArticlesByCurrentUser;
using BlazingBlog.Application.Articles.TogglePublishArticle;
using MediatR;

namespace BlazingBlog.Application.Articles
{
    internal class ArticlesOverviewService : IArticlesOverviewService
    {
        private readonly ISender _sender;

        public ArticlesOverviewService(ISender sender)
        {
            _sender = sender;
        }

        public async Task<List<ArticleResponse>?> GetArticlesByCurrentUserAsync()
        {
            var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
            return result;
        }

        public async Task<ArticleResponse?> TogglePublishArticleAsync(int articleId)
        {
            var result = await _sender.Send(new TogglePublishArticleCommand
            {
                ArticleId = articleId
            });
            return result;
        }
    }
}
