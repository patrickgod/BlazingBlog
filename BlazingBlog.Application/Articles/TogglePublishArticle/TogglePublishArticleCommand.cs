namespace BlazingBlog.Application.Articles.TogglePublishArticle
{
    public class TogglePublishArticleCommand : ICommand<ArticleResponse>
    {
        public int ArticleId { get; set; }
    }
}
