namespace ComputerStore.BLL.Models.CategoryCharacteristics.Double
{
    public class ProductCharacteristicDoubleDto
    {
        public string Name { get; set; }
        public string Dimension { get; set; }
        public double CharacteristicValueDouble { get; set; }

        public int ProductCategoryId { get; set; }
        public int Id { get; set; }
    }
}
