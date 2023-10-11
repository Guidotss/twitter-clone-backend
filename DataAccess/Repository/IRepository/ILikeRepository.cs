using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task RemoveLike(Guid userId, Guid tweetId);
        Task<bool> IsLiked(Guid userId, Guid tweetId);
    }
}
