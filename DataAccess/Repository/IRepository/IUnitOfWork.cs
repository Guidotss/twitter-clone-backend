using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository User { get; }
        public ITweetRepository Tweet { get; }
        public ICommentRepository Comments { get; } 
        public ILikeRepository Like { get; }
        public IRetweetRepository Retweet { get; }
        public Task Save();

    }
}
