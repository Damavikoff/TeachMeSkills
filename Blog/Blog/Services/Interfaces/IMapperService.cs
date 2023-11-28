using Blog.Models;
using Data.Models;

namespace Blog.Services.Interfaces
{
    public interface IMapperService
    {
        ArticleDTO MapToArticleDTO(Article article);
        List<ArticleDTO> MapToArticleDTOList(List<Article> articles);
        Article MapToArticle(ArticleDTO articleDTO);
        Comment MapToComment(CommentDTO commentDTO);

    }
}
