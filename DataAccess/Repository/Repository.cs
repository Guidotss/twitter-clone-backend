using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
        public async Task AddRangeAsync(IEnumerable<T> entity) => await dbSet.AddRangeAsync(entity);
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Guid id) => await dbSet.FindAsync(id);

        public Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string includeProperties = "", bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter); 
            }

            if(includeProperties != null)
            {
                foreach(var includeProps in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProps); 
                }
            }

            if(!isTracking)
            {
                query = query.AsNoTracking(); 
            }

            return query.FirstOrDefaultAsync(); 
        }
        public void RemoveAsync(Guid id)
        {
            var entity = dbSet.Find(id);
            if(entity != null)
            {
                dbSet.Remove(entity);
            }
        }
        public void RemoveRangeAsync(IEnumerable<T> entity) => dbSet.RemoveRange(entity);
        
    }
}
