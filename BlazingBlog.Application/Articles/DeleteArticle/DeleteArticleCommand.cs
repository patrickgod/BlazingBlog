namespace BlazingBlog.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommand : ICommand
    {
        public int Id { get; set; }
    }
}
