namespace Lesson_10
{
    internal class InstagramEmulation
    {
        public event Action<string, string> PostNotify;
        public void GetNotify(string post, string name)
        {
            PostNotify?.Invoke(post, name);
        }
    }
}