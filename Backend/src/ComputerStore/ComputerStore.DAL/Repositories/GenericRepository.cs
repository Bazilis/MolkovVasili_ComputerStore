using ComputerStore.DAL.EF;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsTracking();
        }

        public IQueryable<T> GetAllNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int entityId)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task<T> GetByIdNoTrackingAsync(int entityId)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entityId);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> CreateNoTrackingAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoTrackingAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async Task DeleteAsync(int entityId)
        {
            var entity = await _context.FindAsync<T>(entityId);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
