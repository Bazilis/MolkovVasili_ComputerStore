using System.Collections.Generic;

namespace ComputerStore.BLL.Models.CategoryCharacteristics.String
{
    public class CharacteristicValueStringDto
    {
        public string ValueString { get; set; }
        public int CategoryCharacteristicStringId { get; set; }
        public List<int> ProductIds { get; set; }
        public int Id { get; set; }
    }
}
