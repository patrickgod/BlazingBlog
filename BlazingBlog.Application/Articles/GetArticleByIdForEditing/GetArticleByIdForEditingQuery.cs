namespace BlazingBlog.Application.Articles.GetArticleByIdForEditing
{
    public class GetArticleByIdForEditingQuery : IQuery<ArticleResponse?>
    {
        public int Id { get; set; }
    }
}
