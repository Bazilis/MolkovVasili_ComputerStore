using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ICharacteristicValueStringRepository : IGenericRepository<CharacteristicValueStringEntity>
    {
        Task<CharacteristicValueStringEntity> GetByValueStringAndCharacteristicIdAsync(string valueString,
            int characteristicId);
    }
}
