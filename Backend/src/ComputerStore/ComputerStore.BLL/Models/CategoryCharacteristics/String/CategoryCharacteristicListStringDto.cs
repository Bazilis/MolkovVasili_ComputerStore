using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.String
{
    public class CategoryCharacteristicListStringDto
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        public int ProductCategoryId { get; set; }
        public List<CharacteristicValueStringDto> CharacteristicValuesString { get; set; }
        public int Id { get; set; }
    }
}
