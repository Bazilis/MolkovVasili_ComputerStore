using ComputerStore.BLL.Models;
using ComputerStore.BLL.Models.FilterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerStore.BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsByQueryParamsAsync(
            DoubleFilterModel[] doubleFilterArray,
            IntFilterModel[] intFilterArray,
            StringFilterModel[] stringFilterArray);

        Task<IEnumerable<ProductDto>> GetAllProductsByProductCategoryIdAsync(int productCategoryId);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto> GetByIdWithAllCharacteristicsAsync(int itemId);

        Task<ProductDto> GetByIdAsync(int itemId);

        Task<ProductDto> CreateAsync(ProductDto item);

        Task UpdateAsync(ProductDto item);

        Task DeleteAsync(int itemId);
    }
}
