
namespace SystemStock.RelationalData.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string?  Description { get; set; }
        public long? CategoryId { get; set; }
        public long? Price { get; set; }
        public long? Amount { get; set; }

    }
}
