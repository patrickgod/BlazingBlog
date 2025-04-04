using BlazingBlog.Application.Articles;

namespace BlazingBlog.WebUI.Server.Features.Articles
{
    public static class ArticlesEndpoints
    {
        public static IEndpointRouteBuilder MapArticlesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/articles", async (IArticlesOverviewService articlesOverviewService) =>
            {
                var result = await articlesOverviewService.GetArticlesByCurrentUserAsync();
                return Results.Ok(result);
            });

            app.MapPatch("/api/articles/{id:int}", async (int id, IArticlesOverviewService articlesOverviewService) =>
            {
                var result = await articlesOverviewService.TogglePublishArticleAsync(id);
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });

            return app;
        }
    }
}
