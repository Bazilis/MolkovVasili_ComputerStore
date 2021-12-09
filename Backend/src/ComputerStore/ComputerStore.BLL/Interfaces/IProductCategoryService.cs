using ComputerStore.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();

        Task<ProductCategoryDto> GetByIdAsync(int itemId);

        Task<ProductCategoryDto> CreateAsync(ProductCategoryDto item);

        Task UpdateAsync(ProductCategoryDto item);

        Task DeleteAsync(int itemId);
    }
}
