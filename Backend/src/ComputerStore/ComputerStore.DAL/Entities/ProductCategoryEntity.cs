using ComputerStore.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities
{
    public class ProductCategoryEntity : IEntity
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
