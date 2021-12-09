using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.Int
{
    public interface ICategoryCharacteristicIntService
    {
        Task<List<CategoryCharacteristicIntDto>> GetAllCategoryCharacteristicsIntByProductCategoryIdAsync(int productId);

        Task<List<CategoryCharacteristicIntDto>> GetAllAsync();

        Task<CategoryCharacteristicIntDto> GetByIdAsync(int itemId);

        Task<CategoryCharacteristicIntDto> CreateAsync(CategoryCharacteristicIntDto item);

        Task UpdateAsync(CategoryCharacteristicIntDto item);

        Task DeleteAsync(int itemId);
    }
}
