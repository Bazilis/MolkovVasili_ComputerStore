using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities
{
    public class ProductCategoryEntity : IEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CategoryCharacteristicDoubleEntity> CategoryCharacteristicsDouble { get; set; }
        public virtual ICollection<CategoryCharacteristicIntEntity> CategoryCharacteristicsInt { get; set; }
        public virtual ICollection<CategoryCharacteristicStringEntity> CategoryCharacteristicsString { get; set; }

        public int Id { get; set; }
    }
}
