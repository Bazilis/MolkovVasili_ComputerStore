using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Repositories
{
    public class CharacteristicValueStringRepository : GenericRepository<CharacteristicValueStringEntity>, ICharacteristicValueStringRepository
    {
        private readonly StoreDbContext _context;

        public CharacteristicValueStringRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CharacteristicValueStringEntity> GetByValueStringAndCharacteristicIdAsync(string valueString, int characteristicId)
        {
            var entityResult = await _context.Set<CharacteristicValueStringEntity>()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(x =>
                    x.ValueString == valueString &&
                    x.CategoryCharacteristicStringId == characteristicId);

            return entityResult;
        }
    }
}
