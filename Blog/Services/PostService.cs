using Blog.Models;
using Blog.Services.Interfaces;
using Data.Models;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        BlogContext _blogContext;
        IMapperService _mapper;
        public PostService(BlogContext blogContext, IMapperService mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }
        public void CreateArticle(ArticleDTO articleModel)
        {
            var article = _mapper.MapToArticle(articleModel);
            _blogContext.Articles.Add(article);
            _blogContext.SaveChanges();

        }
        public void CreateComment(CommentDTO commentModel)
        {
            //article validation
            var comment = _mapper.MapToComment(commentModel);
            _blogContext.Comments.Add(comment);
            _blogContext.SaveChanges();
        }
        
    }
}