using ComputerStore.DAL.Entities;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        Task<ProductEntity> GetByIdWithAllCharacteristicsAsync(int productId);
    }
}
