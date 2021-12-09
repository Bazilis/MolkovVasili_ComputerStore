using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.Double
{
    public interface ICategoryCharacteristicDoubleService
    {
        Task<List<CategoryCharacteristicDoubleDto>> GetAllCategoryCharacteristicsDoubleByProductCategoryIdAsync(int productId);

        Task<List<CategoryCharacteristicDoubleDto>> GetAllAsync();

        Task<CategoryCharacteristicDoubleDto> GetByIdAsync(int itemId);

        Task<CategoryCharacteristicDoubleDto> CreateAsync(CategoryCharacteristicDoubleDto item);

        Task UpdateAsync(CategoryCharacteristicDoubleDto item);

        Task DeleteAsync(int itemId);
    }
}
