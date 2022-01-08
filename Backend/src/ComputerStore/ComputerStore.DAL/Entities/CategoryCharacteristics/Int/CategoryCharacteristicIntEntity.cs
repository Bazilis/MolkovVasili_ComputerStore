using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.Int
{
    public class CategoryCharacteristicIntEntity : IEntity
    {
        [Required]
        public string Name { get; set; }
        public string Dimension { get; set; }

        public virtual ICollection<CharacteristicValueIntEntity> CharacteristicValuesInt { get; set; }

        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
