using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ICharacteristicValueIntRepository : IGenericRepository<CharacteristicValueIntEntity>
    {
        Task<CharacteristicValueIntEntity> GetByValueIntAndCharacteristicIdAsync(int valueInt, int characteristicId);

        Task<IEnumerable<int>> GetAllProductsIdsByMinMaxValueIntAndCharacteristicIdAsync(int characteristicId, int? minVal, int? maxVal);
    }
}
