

namespace SystemStock.RelationalData.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<StoreEntity>? Store { get; set; }
    }
}
