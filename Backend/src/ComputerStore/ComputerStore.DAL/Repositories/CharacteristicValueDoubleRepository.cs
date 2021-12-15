using ComputerStore.DAL.EF;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CharacteristicValueDoubleEntity> GetByValueDoubleAndCharacteristicIdAsync(double valueDouble, int characteristicId)
        {
            var entityResult = await _context.Set<CharacteristicValueDoubleEntity>()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(x =>
                    //Math.Abs(x.ValueDouble - value) <= 0.001 &&
                    (x.ValueDouble > valueDouble ? x.ValueDouble - valueDouble : valueDouble - x.ValueDouble) <= 0.001 &&
                    x.CategoryCharacteristicDoubleId == characteristicId);

            return entityResult;
        }
    }
}
