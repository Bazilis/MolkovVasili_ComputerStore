using ComputerStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DAL.Interfaces
{
    public interface ITypedEntity<T> : IEntity
    {
        [Required]
        public T Value { get; set; }
        public int CategoryCharacteristicStringId { get; set; }
        public List<ProductEntity> Products { get; set; }
        public int Id { get; set; }
    }
}
