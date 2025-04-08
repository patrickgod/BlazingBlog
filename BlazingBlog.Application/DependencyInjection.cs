using BlazingBlog.Application.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingBlog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();

            services.AddScoped<IArticlesOverviewService, ArticlesOverviewService>();

            return services;
        }
    }
}
