using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    }
}
