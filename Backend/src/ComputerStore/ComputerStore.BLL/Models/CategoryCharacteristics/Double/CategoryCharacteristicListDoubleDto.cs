using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.Double
{
    public class CategoryCharacteristicListDoubleDto
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        
        public List<CharacteristicValueDoubleDto> CharacteristicValuesDouble { get; set; }

        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
