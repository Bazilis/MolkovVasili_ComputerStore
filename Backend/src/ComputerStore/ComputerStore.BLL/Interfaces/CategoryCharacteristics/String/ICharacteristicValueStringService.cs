using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.String
{
    public interface ICharacteristicValueStringService
    {
        Task<IEnumerable<CharacteristicValueStringDto>> GetAllCharacteristicValuesStringByCategoryCharacteristicStringIdAsync(int categoryCharacteristicStringId);

        Task<IEnumerable<CharacteristicValueStringDto>> GetAllAsync();

        Task<CharacteristicValueStringDto> GetByIdAsync(int itemId);

        Task<CharacteristicValueStringDto> GetByValueStringAndCharacteristicIdAsync(string value, int id);

        Task<CharacteristicValueStringDto> CreateAsync(CharacteristicValueStringDto item);

        Task UpdateAsync(CharacteristicValueStringDto item);

        Task DeleteAsync(int itemId);
    }
}
