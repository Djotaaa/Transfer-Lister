using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Transfer_ListerAPI.Data;
using Transfer_ListerAPI.Repository.IRepository;

namespace Transfer_ListerAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeEntities = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeEntities != null)
            {
                var entities = includeEntities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entity in entities)
                {
                    query = query.Include(entity);
                }
            }

            return await query.ToListAsync();


        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>>? filter = null, string? includeEntities = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeEntities != null)
            {
                var entities = includeEntities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entity in entities)
                {
                    query = query.Include(entity);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
