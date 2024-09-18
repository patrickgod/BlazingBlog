using BlazingBlog.Application.Articles;
using BlazingBlog.Application.Articles.GetArticlesByCurrentUser;
using BlazingBlog.Application.Articles.TogglePublishArticle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazingBlog.WebUI.Server.Features.Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ISender _sender;

        public ArticlesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArticleResponse>>> GetArticlesByCurrentUser()
        {
            var result = await _sender.Send(new GetArticlesByCurrentUserQuery());
            return Ok(result.Value);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ArticleResponse>> TogglePublishArticle(int id)
        {
            var result = await _sender.Send(new TogglePublishArticleCommand { ArticleId = id });
            if (result.Failure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
