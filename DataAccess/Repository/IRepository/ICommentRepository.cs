using Models;
using DataTransfer;

namespace DataAccess.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public void Update(Comment comment);
        public Task<List<UserCommentDto>> GetCommentUsers(Guid tweetId);
    }
}
