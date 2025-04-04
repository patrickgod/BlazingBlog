using BlazingBlog.Application.Articles;

namespace BlazingBlog.WebUI.Server.Features.Articles
{
    public static class ArticlesEndpoints
    {
        public static IEndpointRouteBuilder MapArticlesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/articles").WithTags("Articles");

            group.MapGet("/", async (IArticlesOverviewService articlesOverviewService) =>
            {
                var result = await articlesOverviewService.GetArticlesByCurrentUserAsync();
                return Results.Ok(result);
            });

            group.MapPatch("/{id:int}", async (int id, IArticlesOverviewService articlesOverviewService) =>
            {
                var result = await articlesOverviewService.TogglePublishArticleAsync(id);
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });

            return app;
        }
    }
}
