using Blog.Models;

namespace Blog.Services
{
    public interface IPostService
    {
        void AddPost(Post post);
        List<Post> GetPosts();
        Post GetPost(int id);
    }
}