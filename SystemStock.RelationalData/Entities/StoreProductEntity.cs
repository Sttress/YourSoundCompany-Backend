
namespace SystemStock.RelationalData.Entities
{
    public class StoreProductEntity
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public long StoreId { get; set; }
        public StoreEntity Store { get; set; }
        public long Amount { get; set; }
    }
}
