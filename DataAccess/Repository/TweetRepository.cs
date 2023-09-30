using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TweetRepository : Repository<Tweet>, ITweetRepository
    {
        private readonly ApplicationDbContext _db;
        public TweetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Tweet tweet)
        {
            var tweetFromDb = _db.Tweet.FirstOrDefault(t => t.Id == tweet.Id);
            if (tweetFromDb != null)
            {
                tweetFromDb.Content = tweet.Content ?? tweetFromDb.Content;
                tweetFromDb.Likes = tweet.Likes ?? tweetFromDb.Likes;
                tweetFromDb.Retweets = tweet.Retweets ?? tweetFromDb.Retweets;
                tweetFromDb.Comments = tweet.Comments ?? tweetFromDb.Comments;
                _db.SaveChanges();
            }
        }
    }
}
