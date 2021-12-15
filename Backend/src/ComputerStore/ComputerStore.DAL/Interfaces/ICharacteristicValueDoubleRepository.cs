using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ICharacteristicValueDoubleRepository : IGenericRepository<CharacteristicValueDoubleEntity>
    {
        Task<CharacteristicValueDoubleEntity> GetByValueDoubleAndCharacteristicIdAsync(double valueDouble,
            int characteristicId);
    }
}
