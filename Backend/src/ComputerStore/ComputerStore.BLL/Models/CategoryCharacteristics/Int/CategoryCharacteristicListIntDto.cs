using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.Int
{
    public class CategoryCharacteristicListIntDto
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        
        public List<CharacteristicValueIntDto> CharacteristicValuesInt { get; set; }

        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
