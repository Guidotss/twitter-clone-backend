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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
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
