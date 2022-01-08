using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.Double
{
    public class CharacteristicValueDoubleEntity : IEntity
    {
        public double ValueDouble { get; set; }

        public List<ProductEntity> Products { get; set; } = new();

        public int CategoryCharacteristicDoubleId { get; set; }
        public int Id { get; set; }
    }
}
