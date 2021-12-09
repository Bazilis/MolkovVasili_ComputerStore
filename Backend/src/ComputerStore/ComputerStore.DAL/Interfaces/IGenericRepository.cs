using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int entityId);

        Task<T> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int entityId);
    }
}
