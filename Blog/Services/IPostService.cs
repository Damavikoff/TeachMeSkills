using Blog.Models;

namespace Blog.Services
{
    public interface IPostService
    {
        void AddPost(Article article);
        void AddComment(Comment comment);
        List<Article> GetPosts();
        Article GetPost(Guid id);
        Article GetComments(Guid id);
    }
}