namespace Lesson_10
{
    internal class User
    {
        public int myPostsLimit { get; set; }
        public int postsToSeeLimit { get; set; }
        private string[] myPosts;
        private string[] postsToSee;
        public string Name { get; set; }
        public User(int myPostsLimit, int postsToSeeLimit, string name)
        {
            this.myPostsLimit = myPostsLimit;
            this.postsToSeeLimit = postsToSeeLimit;
            Name = name;
            myPosts = new string[myPostsLimit];
            postsToSee = new string[postsToSeeLimit];
        }
        public void AddPost(InstagramEmulation instagram)
        {
            int index = 0;
            int check = 0;
            string post = Name + " " + DateTime.Now;
            foreach (var myPost in myPosts)
            {
                if (myPost == null)
                {
                    myPosts[index] = post;
                    instagram.GetNotify(myPosts[index], Name);
                    check++;
                    break;
                }
                index++;
            }
            if (check == 0)
            {
                instagram.GetNotify(post, Name);
            }
        }
        public void AddPostToSee(string post, string name)
        {
            if (Name != name)
            {
                int index = 0;
                foreach (var postToSee in postsToSee)
                {
                    if (postToSee == null)
                    {
                        postsToSee[index] = post;
                        break;
                    }
                    index++;
                }
            }
        }
    }
}