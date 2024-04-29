using BlazingBlog.Application.Articles.CreateArticle;
using BlazingBlog.Domain.Articles;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.UpdateArticle
{
    public class UpdateArticleCommandHandler : ICommandHandler<UpdateArticleCommand, ArticleResponse?>
    {
        private readonly IArticleRepository _articleRepository;

        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<Result<ArticleResponse?>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var updatedArticle = request.Adapt<Article>();
            var article = await _articleRepository.UpdateArticleAsync(updatedArticle);
            if(article is null)
                return Result.Fail<ArticleResponse?>("Article does not exist.");
            
            return article.Adapt<ArticleResponse>();
        }
    }
}
