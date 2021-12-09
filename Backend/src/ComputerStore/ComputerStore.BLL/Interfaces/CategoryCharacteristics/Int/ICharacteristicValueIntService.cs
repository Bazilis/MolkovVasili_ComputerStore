using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.Int
{
    public interface ICharacteristicValueIntService
    {
        Task<List<CharacteristicValueIntDto>> GetAllCharacteristicValuesIntByCategoryCharacteristicIntIdAsync(int categoryCharacteristicIntId);

        Task<List<CharacteristicValueIntDto>> GetAllAsync();

        Task<CharacteristicValueIntDto> GetByIdAsync(int itemId);

        Task<CharacteristicValueIntDto> CreateAsync(CharacteristicValueIntDto item);

        Task UpdateAsync(CharacteristicValueIntDto item);

        Task DeleteAsync(int itemId);
    }
}
