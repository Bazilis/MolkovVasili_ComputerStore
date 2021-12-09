using ComputerStore.BLL.Models.CategoryCharacteristics.Double;
using ComputerStore.BLL.Models.CategoryCharacteristics.Int;
using ComputerStore.BLL.Models.CategoryCharacteristics.String;
using System.Collections.Generic;

namespace ComputerStore.BLL.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int QuantityInStorage { get; set; }
        public int ProductCategoryId { get; set; }
        public List<ProductCharacteristicDoubleDto> ProductCharacteristicsDouble { get; set; }
        public List<ProductCharacteristicIntDto> ProductCharacteristicsInt { get; set; }
        public List<ProductCharacteristicStringDto> ProductCharacteristicsString { get; set; }
        public int Id { get; set; }
    }
}
