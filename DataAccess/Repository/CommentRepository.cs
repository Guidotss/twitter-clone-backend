using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public Task<List<User>>GetCommentUsers(Guid tweetId)
        {
           
            var comments = _db.Comments.Where(c => c.TweetId == tweetId).AsEnumerable();
            //Evita traer la infomacion del password

            var users = _db.User.Where(u => comments.Any(c => c.UserId == u.Id)).AsEnumerable().Select(u => new User
            {   
                Id = u.Id,
                Name = u.Name,
                Email = u.Email, 
                ImageUrl = u.ImageUrl,
                Comments = u.Comments,
            }); 
            
            

            return Task.FromResult(users.ToList());
        }

        public void Update(Comment comment)
        {
            var commentFromDb = _db.Comments.FirstOrDefault(c => c.Id == comment.Id);
            if(commentFromDb != null) 
            {
                commentFromDb.Content = comment.Content; 
                commentFromDb.UpdatedAt = DateTime.Now;
                _db.SaveChanges();
            }
        }


    }
}
