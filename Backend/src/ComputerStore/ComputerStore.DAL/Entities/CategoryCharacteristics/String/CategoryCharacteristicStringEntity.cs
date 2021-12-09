using ComputerStore.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DAL.Entities.CategoryCharacteristics.String
{
    public class CategoryCharacteristicStringEntity : IEntity
    {
        [Required]
        public string Name { get; set; }
        public string Dimension { get; set; }
        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
