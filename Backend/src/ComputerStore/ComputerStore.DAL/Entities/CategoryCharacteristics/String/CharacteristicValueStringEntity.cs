using ComputerStore.DAL.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.String
{
    public class CharacteristicValueStringEntity : IEntity
    {
        [Required]
        public string ValueString { get; set; }
        public int CategoryCharacteristicStringId { get; set; }
        public List<ProductEntity> Products { get; set; }
        public int Id { get; set; }
    }
}
