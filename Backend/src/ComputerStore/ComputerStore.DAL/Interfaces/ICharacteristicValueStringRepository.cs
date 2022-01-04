using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ICharacteristicValueStringRepository : IGenericRepository<CharacteristicValueStringEntity>
    {
        Task<CharacteristicValueStringEntity> GetByValueStringAndCharacteristicIdAsync(string valueString, int characteristicId);

        Task<IEnumerable<int>> GetAllProductsIdsByValueStringAndCharacteristicIdAsync(int characteristicId, string valueString);
    }
}
