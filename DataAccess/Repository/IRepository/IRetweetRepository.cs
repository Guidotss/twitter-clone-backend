using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IRetweetRepository : IRepository<Retweet>
    {
        Task<Retweet> GetRetweetByUserAndTweet(Guid userId, Guid tweetId);
        Task<IEnumerable<Retweet>> GetRetweetsByTweet(Guid tweetId);
    }
}
