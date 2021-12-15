using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ICharacteristicValueIntRepository : IGenericRepository<CharacteristicValueIntEntity>
    {
        Task<CharacteristicValueIntEntity> GetByValueIntAndCharacteristicIdAsync(int valueInt, int characteristicId);
    }
}
