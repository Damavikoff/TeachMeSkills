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
            var articles = _blogContext.Articles.ToList(); //Include(p => p.Account) IMapperService
            //как селектить статью+имя автора из другой таблицы? динамическая модель (из-за анонимного типа)? передавать модель+вьюбаг? 
            //var articles = _blogContext.Articles.Select(p => new
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Content = p.Content,
            //    Description=p.Description,
            //    CreatedAt = p.CreatedAt,
            //    AccountId = p.Account.Name
            //});
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

        public List<ArticleDetailDTO> ShowArticleList()
        {
            var articles = _blogContext.Articles.Include(e => e.Account).Select(e =>
              new ArticleDetailDTO()
              {
                  Id = e.Id,
                  Title = e.Title,
                  Description = e.Description,
                  CreatedAt = e.CreatedAt,
                  AccountName = e.Account.Name,
                  Content = e.Content
              }).ToList(); //(b => b.Id == articleId);



            return articles;
        }
    }
}
