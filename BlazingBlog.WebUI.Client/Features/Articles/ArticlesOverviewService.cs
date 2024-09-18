using BlazingBlog.Application.Articles;
using System.Net.Http.Json;

namespace BlazingBlog.WebUI.Client.Features.Articles
{
    public class ArticlesOverviewService : IArticlesOverviewService
    {
        private readonly HttpClient _http;

        public ArticlesOverviewService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ArticleResponse>?> GetArticlesByCurrentUserAsync()
        {
            return await _http.GetFromJsonAsync<List<ArticleResponse>>("/api/Articles");
        }

        public async Task<ArticleResponse?> TogglePublishArticleAsync(int articleId)
        {
            var result = await _http.PatchAsync($"api/Articles/{articleId}", null);
            if (result is not null && result.Content is not null)
                return await result.Content.ReadFromJsonAsync<ArticleResponse>();

            return null;
        }
    }
}
