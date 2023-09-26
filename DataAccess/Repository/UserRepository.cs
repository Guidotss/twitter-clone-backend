using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models; 

namespace DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public async Task<User> GetUserByEmail(string email) => await _db.User.FirstOrDefaultAsync(u => u.Email == email);
        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool VerifyPassword(User user, string password) => BCrypt.Net.BCrypt.Verify(password, user.Password);


        public async void Update(User user)
        {
            var userFromDb = _db.User.FirstOrDefault(u => u.Id == user.Id);
            if(userFromDb != null)
            {
                userFromDb.Name = user.Name;
                userFromDb.Email = user.Email;
                userFromDb.Password = user.Password;
                userFromDb.ImageUrl = user.ImageUrl;
                userFromDb.Bio = user.Bio;
                await _db.SaveChangesAsync();
            }
        }

    }
}; 
