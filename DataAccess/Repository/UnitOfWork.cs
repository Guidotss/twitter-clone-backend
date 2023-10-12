using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository User { get; private set; }
        public ITweetRepository Tweet { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ILikeRepository Like { get; private set; }
        public IRetweetRepository Retweet { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _context = db;
            User = new UserRepository(_context);
            Tweet = new TweetRepository(_context);
            Comments = new CommentRepository(_context);
            Like = new LikeRepository(_context);
            Retweet = new RetweetRepository(_context);
        }

        public void Dispose() => _context.Dispose();
        

        public async Task Save() => await _context.SaveChangesAsync();
        
    }
}
