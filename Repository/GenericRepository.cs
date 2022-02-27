using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext databaseContext;
        private readonly DbSet<T> db;
        public GenericRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.db = this.databaseContext.Set<T>();
        }


        public async Task Delete(int id)
        {
            var entity = await this.db.FindAsync(id);
            if (entity != null)
            {
                db.Remove(entity);
            }
            else
            {
                //TODO    
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            databaseContext.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = db;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await databaseContext.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await databaseContext.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            db.Attach(entity);
            databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
