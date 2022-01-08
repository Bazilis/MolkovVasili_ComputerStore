using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.Int
{
    public class CharacteristicValueIntDto
    {
        public int ValueInt { get; set; }
        
        public List<int> ProductIds { get; set; }

        public int CategoryCharacteristicIntId { get; set; }
        public int Id { get; set; }
    }
}
