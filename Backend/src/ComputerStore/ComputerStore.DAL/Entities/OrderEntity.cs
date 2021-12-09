using ComputerStore.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.DAL.Entities
{
    public class OrderEntity : IEntity
    {
        [Required]
        public string UserId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public int OrderStatus { get; set; }
        public int Id { get; set; }
    }
}
