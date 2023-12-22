
namespace SystemStock.RelationalData.Entities
{
    public class BoxEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public List<ProductEntity>? Products { get; set; }
        public string? Description { get; set; }
    }
}
