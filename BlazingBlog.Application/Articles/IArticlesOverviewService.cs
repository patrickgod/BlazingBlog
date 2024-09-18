namespace BlazingBlog.Application.Articles
{
    public interface IArticlesOverviewService
    {
        Task<ArticleResponse?> TogglePublishArticleAsync(int articleId);
        Task<List<ArticleResponse>?> GetArticlesByCurrentUserAsync();
    }
}
