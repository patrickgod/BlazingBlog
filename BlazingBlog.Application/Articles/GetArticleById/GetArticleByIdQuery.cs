namespace BlazingBlog.Application.Articles.GetArticleById
{
    public class GetArticleByIdQuery : IQuery<ArticleResponse?>
    {
        public int Id { get; set; }
    }
}
