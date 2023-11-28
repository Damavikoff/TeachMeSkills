using Lesson_10;

User user1 = new User(10, 5, "user1");
User user2 = new User(10, 5, "user2");

InstagramEmulation instagram = new InstagramEmulation();
instagram.PostNotify += user1.AddPostToSee;
instagram.PostNotify += user2.AddPostToSee;

user1.AddPost(instagram);
user2.AddPost(instagram);