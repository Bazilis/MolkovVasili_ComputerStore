using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double
{
    public interface ICharacteristicValueDoubleService
    {
        //Task<List<CharacteristicValueDoubleDto>> GetAllCharacteristicValuesDoubleByProductIdAsync(int productId);

        Task<List<CharacteristicValueDoubleDto>> GetAllCharacteristicValuesDoubleByCategoryCharacteristicDoubleIdAsync(int categoryCharacteristicDoubleId);

        Task<List<CharacteristicValueDoubleDto>> GetAllAsync();

        Task<CharacteristicValueDoubleDto> GetByIdAsync(int itemId);

        Task<CharacteristicValueDoubleDto> CreateAsync(CharacteristicValueDoubleDto item);

        Task UpdateAsync(CharacteristicValueDoubleDto item);

        Task DeleteAsync(int itemId);
    }
}
