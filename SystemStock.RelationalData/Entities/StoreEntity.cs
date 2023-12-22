

namespace SystemStock.RelationalData.Entities
{
    public class StoreEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? ColorPrimary { get; set; }
        public string? ColorSecondary { get; set; }
        public long UserId { get; set; }
        public UserEntity? User { get; set; }

    }
}
