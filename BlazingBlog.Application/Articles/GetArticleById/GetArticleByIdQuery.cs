using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBlog.Application.Articles.GetArticleById
{
    public class GetArticleByIdQuery : IRequest<ArticleResponse?>
    {
        public int Id { get; set; }
    }
}
