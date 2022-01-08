using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Repositories
{
    public class CharacteristicValueDoubleRepository : GenericRepository<CharacteristicValueDoubleEntity>, ICharacteristicValueDoubleRepository
    {
        private readonly StoreDbContext _context;

        public CharacteristicValueDoubleRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CharacteristicValueDoubleEntity> GetByValueDoubleAndCharacteristicIdAsync(
            double valueDouble, int characteristicId)
        {
            var entityResult = await _context.Set<CharacteristicValueDoubleEntity>()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(x =>
                    (x.ValueDouble > valueDouble ? x.ValueDouble - valueDouble : valueDouble - x.ValueDouble) <= 0.001 &&
                    x.CategoryCharacteristicDoubleId == characteristicId);

            return entityResult;
        }

        public async Task<IEnumerable<int>> GetAllProductsIdsByMinMaxValueDoubleAndCharacteristicIdAsync(
            int characteristicId, double? minVal, double? maxVal)
        {
            var entityResult = await _context.Set<CharacteristicValueDoubleEntity>()
                .Include(c => c.Products)
                .Where(x =>
                    x.CategoryCharacteristicDoubleId == characteristicId &&
                    x.ValueDouble >= minVal &&
                    x.ValueDouble <= maxVal)
                .SelectMany(с => с.Products.Select(p => p.Id))
                .Distinct()
                .ToArrayAsync();

            return entityResult;
        }
    }
}
