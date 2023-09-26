using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models; 

namespace DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public void Update(User user);
        public string HashPassword(string password);
        public bool VerifyPassword(User user,string password);
        public Task<User> GetUserByEmail(string email);   
    }
}
