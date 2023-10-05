using Models;

namespace twitter_clone.Custom
{
    public class CustomTweetResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int LikesCount { get; set; }
        public int RetweetsCount { get; set; }
        public User User { get; set; } = new User();
        

    }
}
