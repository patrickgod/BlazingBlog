using BlazingBlog.Application.Articles;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IArticlesOverviewService,
    BlazingBlog.WebUI.Client.Features.Articles.ArticlesOverviewService>();

await builder.Build().RunAsync();
