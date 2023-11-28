using Blog.Models;

namespace Blog.Services.Interfaces
{
    public interface IReadService
    {
        List<ArticleDTO> ShowArticles();
        ArticleDTO ShowArticleDetails(Guid articleId);




        List<ArticleDetailDTO> ShowArticleList();
    }
}
