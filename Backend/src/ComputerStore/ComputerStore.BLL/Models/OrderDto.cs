namespace ComputerStore.BLL.Models
{
    public class OrderDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Status OrderStatus { get; set; }
        public int Id { get; set; }

        public enum Status
        {
            Created,
            Executing,
            Completed
        }
    }
}
