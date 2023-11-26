using Blog.Models;
using Data.Models;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class ReadService : IReadService
    {
        BlogContext _blogContext;
        IMapperService _mapper;
        public ReadService(BlogContext blogContext, IMapperService mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }
        public List<ArticleDTO> ShowArticles()
        {
            var articles = _blogContext.Articles.ToList();
            
            var articlesDTO = _mapper.MapToArticleDTOList(articles);

            return articlesDTO;
        }
        public ArticleDTO ShowArticleDetails(Guid articleId)
        {
            var article = _blogContext.Articles.Include(p => p.Comments)
                                               .SingleOrDefault(p => p.Id == articleId);

            var articleDTO = _mapper.MapToArticleDTO(article);

            return articleDTO;
        }
    }
}
