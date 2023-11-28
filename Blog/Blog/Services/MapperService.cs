using Blog.Models;
using Blog.Services.Interfaces;
using Data.Models;

namespace Blog.Services
{
    public class MapperService : IMapperService //AutoMapper
    {
        public ArticleDTO MapToArticleDTO(Article article)
        {
            var articleDTO = new ArticleDTO()
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                CreatedAt = article.CreatedAt,
                AccountId = article.AccountId,
                Content = article.Content,
                Comments = (from e in article.Comments
                            select new CommentDTO
                            {
                                Id = e.Id,
                                Content = e.Content,
                                AccountId = e.AccountId,
                                ArticleId = e.ArticleId
                            }).ToList()
            };
            return articleDTO;
        }
        public List<ArticleDTO> MapToArticleDTOList(List<Article> articles)
        {
            var articlesDTO = new List<ArticleDTO>
                (from e in articles
                 select new ArticleDTO
                 {
                     Id = e.Id,
                     Title = e.Title,
                     Description = e.Description,
                     CreatedAt = e.CreatedAt,
                     AccountId = e.AccountId,
                     Content = e.Content
                 }).ToList();

            return articlesDTO;
        }

        public Article MapToArticle(ArticleDTO articleDTO)
        {
            var article = new Article()
            {
                Id = articleDTO.Id,
                Title = articleDTO.Title,
                Description = articleDTO.Description,
                CreatedAt = articleDTO.CreatedAt,
                AccountId = articleDTO.AccountId,
                Content = articleDTO.Content
            };
            return article;
        }
        public Comment MapToComment(CommentDTO commentDTO)
        {
            var comment = new Comment()
            {
                Id = commentDTO.Id,
                ArticleId = commentDTO.ArticleId,
                AccountId = commentDTO.AccountId,
                Content = commentDTO.Content
            };
            return comment;
        }
    }
}
