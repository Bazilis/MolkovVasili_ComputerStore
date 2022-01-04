namespace ComputerStore.BLL.Models.FilterModels
{
    public class DoubleFilterModel
    {
        public int CatId { get; set; }

        public double? MinVal { get; set; }

        public double? MaxVal { get; set; }
    }
}
