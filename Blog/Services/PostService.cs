using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        BlogContext _blogContext;
        public PostService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public void AddPost(Article article)
        {
            _blogContext.Articles.Add(article);
            _blogContext.SaveChanges();

        }
        public void AddComment(Comment comment)
        {
            _blogContext.Comments.Add(comment);
            _blogContext.SaveChanges();

        }
        public List<Article> GetPosts()
        {
            var Posts = _blogContext.Articles.ToList();
            return Posts;
        }
        public Article GetPost(Guid id)
        {
            Article obj = null;
            obj = _blogContext.Articles.FirstOrDefault(p => p.Id == id);
            return obj;
        }
        public Article GetComments(Guid id)
        {
            Article obj = null;
             obj = _blogContext.Articles.Include(p => p.Comments)
                                         .Where(p => p.Comments.Any(pc => pc.ArticleId == id))
                                         .SingleOrDefault();
            return obj;
        }
    }
}