using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.String
{
    public class ProductCharacteristicStringDto
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        public int ProductCategoryId { get; set; }
        public string CharacteristicValueString { get; set; }
        public int Id { get; set; }
    }
}
