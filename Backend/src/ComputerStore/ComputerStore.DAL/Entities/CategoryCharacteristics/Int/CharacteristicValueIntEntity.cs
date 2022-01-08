using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.Int
{
    public class CharacteristicValueIntEntity : IEntity
    {
        public int ValueInt { get; set; }

        public List<ProductEntity> Products { get; set; } = new();

        public int CategoryCharacteristicIntId { get; set; }
        public int Id { get; set; }
    }
}
