

namespace YourSoundCompnay.RelationalData.Entities
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active {  get; set; }
        public string? Token { get; set; }
        public string? RefreshToken {  get; set; }
        public string? ClientId { get; set; }
    }
}
