using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces.CategoryCharacteristics.String
{
    public interface ICategoryCharacteristicStringService
    {
        Task<IEnumerable<CategoryCharacteristicStringDto>> GetAllCategoryCharacteristicsStringByProductCategoryIdAsync(int productId);

        Task<IEnumerable<CategoryCharacteristicStringDto>> GetAllAsync();

        Task<CategoryCharacteristicStringDto> GetByIdAsync(int itemId);

        Task<CategoryCharacteristicStringDto> CreateAsync(CategoryCharacteristicStringDto item);

        Task UpdateAsync(CategoryCharacteristicStringDto item);

        Task DeleteAsync(int itemId);
    }
}
