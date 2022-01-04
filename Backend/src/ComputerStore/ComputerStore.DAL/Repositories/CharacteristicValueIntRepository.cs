using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Repositories
{
    public class CharacteristicValueIntRepository : GenericRepository<CharacteristicValueIntEntity>, ICharacteristicValueIntRepository
    {
        private readonly StoreDbContext _context;

        public CharacteristicValueIntRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CharacteristicValueIntEntity> GetByValueIntAndCharacteristicIdAsync(int valueInt, int characteristicId)
        {
            var entityResult = await _context.Set<CharacteristicValueIntEntity>()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(x =>
                    x.ValueInt == valueInt &&
                    x.CategoryCharacteristicIntId == characteristicId);

            return entityResult;
        }

        public async Task<IEnumerable<int>> GetAllProductsIdsByMinMaxValueIntAndCharacteristicIdAsync(
            int characteristicId, int? minVal, int? maxVal)
        {
            var entityResult = await _context.Set<CharacteristicValueIntEntity>()
                .Include(c => c.Products)
                .Where(x =>
                    x.CategoryCharacteristicIntId == characteristicId &&
                    x.ValueInt >= minVal &&
                    x.ValueInt <= maxVal)
                .SelectMany(с => с.Products.Select(p => p.Id))
                .Distinct()
                .ToArrayAsync();

            return entityResult;
        }
    }
}
