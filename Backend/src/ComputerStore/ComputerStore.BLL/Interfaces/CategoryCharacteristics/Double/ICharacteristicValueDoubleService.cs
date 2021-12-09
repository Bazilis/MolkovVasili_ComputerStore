using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double
{
    public interface ICharacteristicValueDoubleService
    {
        Task<IEnumerable<CharacteristicValueDoubleDto>> GetAllCharacteristicValuesDoubleByCategoryCharacteristicDoubleIdAsync(int categoryCharacteristicDoubleId);

        Task<IEnumerable<CharacteristicValueDoubleDto>> GetAllAsync();

        Task<CharacteristicValueDoubleDto> GetByIdAsync(int itemId);

        Task<CharacteristicValueDoubleDto> GetByValueDoubleAndCharacteristicIdAsync(double value, int id);

        Task<CharacteristicValueDoubleDto> CreateAsync(CharacteristicValueDoubleDto item);

        Task UpdateAsync(CharacteristicValueDoubleDto item);

        Task DeleteAsync(int itemId);
    }
}
