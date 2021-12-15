using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllNoTracking();

        Task<T> GetByIdAsync(int entityId);

        Task<T> GetByIdNoTrackingAsync(int entityId);

        Task<T> CreateAsync(T entity);

        Task<T> CreateNoTrackingAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateNoTrackingAsync(T entity);

        Task DeleteAsync(int entityId);
    }
}
