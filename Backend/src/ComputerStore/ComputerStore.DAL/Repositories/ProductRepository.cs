using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductEntity> GetByIdWithAllCharacteristicsAsync(int productId)
        {
            return await _context.Set<ProductEntity>()
                .Include(p => p.CategoryCharacteristicsDouble)
                .Include(p => p.CategoryCharacteristicsInt)
                .Include(p => p.CategoryCharacteristicsString)
                .FirstOrDefaultAsync(e => e.Id == productId);
        }

        public async Task<ProductEntity> GetAllWithAllCharacteristicsAsync(int productId)
        {
            return await _context.Set<ProductEntity>()
                .Include(p => p.CategoryCharacteristicsDouble)
                .Include(p => p.CategoryCharacteristicsInt)
                .Include(p => p.CategoryCharacteristicsString)
                .FirstOrDefaultAsync(e => e.Id == productId);
        }
    }
}
