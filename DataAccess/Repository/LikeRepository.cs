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
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly ApplicationDbContext _db;
        
        public LikeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
            
        }

        public Task<bool> IsLiked(Guid userId, Guid tweetId)
        {
             var like = _db.Likes.FirstOrDefault(l => l.UserId == userId && l.TweetId == tweetId);
            if(like != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task RemoveLike(Guid userId, Guid tweetId)
        {
            var like = _db.Likes.FirstOrDefault(l => l.UserId == userId && l.TweetId == tweetId);
            if(like != null)
            {
                _db.Likes.Remove(like);
                _db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
