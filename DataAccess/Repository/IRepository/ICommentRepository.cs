using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public void Update(Comment comment);
        public Task<List<User>> GetCommentUsers(Guid tweetId);
    }
}
