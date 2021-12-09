using ComputerStore.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Interfaces
{
    public interface ITypedEntity<T> : IEntity
    {
        [Required]
        public T Value { get; set; }
        public int CategoryCharacteristicId { get; set; }
        public List<ProductEntity> Products { get; set; }
    }
}
