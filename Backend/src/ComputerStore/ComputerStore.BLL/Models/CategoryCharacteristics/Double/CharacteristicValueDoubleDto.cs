using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.Double
{
    public class CharacteristicValueDoubleDto
    {
        public double ValueDouble { get; set; }
        
        public List<int> ProductIds { get; set; }

        public int CategoryCharacteristicDoubleId { get; set; }
        public int Id { get; set; }
    }
}
