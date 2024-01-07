
namespace SystemStock.RelationalData.Entities
{
    public class CategoryEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? UserId { get; set; }
        public UserEntity? User {  get; set; }
        public bool Active { get; set; }
    }
}
