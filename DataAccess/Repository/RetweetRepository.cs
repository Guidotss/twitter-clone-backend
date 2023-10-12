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
    public class RetweetRepository : Repository<Retweet>, IRetweetRepository
    {   
        private readonly ApplicationDbContext _db;
        public RetweetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public Task<Retweet> GetRetweetByUserAndTweet(Guid userId, Guid tweetId)
        {
            try
            {
                Retweet? retweet = _db.Retweets.FirstOrDefault(x => x.UserId == userId && x.TweetId == tweetId);
                return Task.FromResult(retweet)!;
            }
            catch (Exception)
            {
                throw new Exception("Error while getting retweet");
            }
        }

        public Task<IEnumerable<Retweet>> GetRetweetsByTweet(Guid tweetId)
        {
            try
            {
                Retweet[]? retweets = _db.Retweets.Where(x => x.TweetId == tweetId).ToArray();
                return Task.FromResult(retweets.AsEnumerable());
            }
            catch(Exception)
            {
                throw new Exception("Error while getting retweets"); 
            }
        }
    }
}
