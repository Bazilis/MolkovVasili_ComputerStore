using ComputerStore.DAL.Entities.CategoryCharacteristics.Double;
using ComputerStore.DAL.Entities.CategoryCharacteristics.Int;
using ComputerStore.DAL.Entities.CategoryCharacteristics.String;
using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.DAL.Entities
{
    public class ProductEntity : IEntity
    {
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int QuantityInStorage { get; set; }
        public int ProductCategoryId { get; set; }
        public List<CharacteristicValueDoubleEntity> CategoryCharacteristicsDouble { get; set; }
        public List<CharacteristicValueIntEntity> CategoryCharacteristicsInt { get; set; }
        public List<CharacteristicValueStringEntity> CategoryCharacteristicsString { get; set; }
        public int Id { get; set; }
    }
}
