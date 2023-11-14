using Blog.Models;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        public void AddPost(Post post)
        {
            using (BlogContext db = new BlogContext())
            {
                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
        public List<Post> GetPosts()
        {
            using (BlogContext db = new BlogContext())
            {
                var Posts = db.Posts.ToList();
                return Posts;
            }
        }
        public Post GetPost(int id)
        {
            Post obj = null;
            using (BlogContext db = new BlogContext())
            {
                obj = db.Posts.FirstOrDefault(p => p.Id == id);
            }
            return obj;
        }
    }
}