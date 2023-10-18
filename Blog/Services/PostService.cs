using Blog.Models;
using System.Text.Json;

namespace Blog.Services
{
    public class PostService : IPostService
    {
        private readonly string _jsonFile = "posts.json";
        public void AddPost(Post post) 
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            var posts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(_jsonFile))
                ?? new List<Post>();
            posts.Add(post);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(_jsonFile, JsonSerializer.Serialize(posts, options));
        }
        public List<Post> GetPosts()
        {
            return JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(_jsonFile))
                ?? new List<Post>();
        }
        public Post GetPost(int id)
        {
            var posts = JsonSerializer.Deserialize<List<Post>>(File.ReadAllText(_jsonFile));
            Post obj = null;
            foreach (var post in posts)
            {
                if (post.Id == id) obj = post;
            }
            return obj; //?? new Post()
        }
    }
}