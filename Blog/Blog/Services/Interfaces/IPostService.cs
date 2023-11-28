using Blog.Models;

namespace Blog.Services.Interfaces
{
    public interface IPostService
    {
        void CreateArticle(ArticleDTO article);
        void CreateComment(CommentDTO comment);
    }
}