

namespace SystemStock.Business.Model.Product
{
    public class ProductModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public bool Active { get; set; }
    }
}
