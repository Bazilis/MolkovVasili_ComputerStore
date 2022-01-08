using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.Double
{
    public class CategoryCharacteristicDoubleEntity : IEntity
    {
        [Required]
        public string Name { get; set; }
        public string Dimension { get; set; }

        public virtual ICollection<CharacteristicValueDoubleEntity> CharacteristicValuesDouble { get; set; }

        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
