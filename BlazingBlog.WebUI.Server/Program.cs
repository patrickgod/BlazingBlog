using BlazingBlog.Application;
using BlazingBlog.Application.Articles;
using BlazingBlog.Infrastructure;
using BlazingBlog.WebUI.Server;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

//app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazingBlog.WebUI.Client._Imports).Assembly);

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

app.Run();
