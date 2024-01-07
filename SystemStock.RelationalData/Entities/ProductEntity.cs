
namespace SystemStock.RelationalData.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string?  Description { get; set; }
        public CategoryEntity Category { get; set; }
        public long? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public bool Active { get; set; }
        public long UserId {  get; set; }
        public UserEntity User { get; set; }

    }
}
