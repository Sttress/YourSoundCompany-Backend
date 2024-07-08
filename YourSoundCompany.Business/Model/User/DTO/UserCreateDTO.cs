namespace YourSoundCompany.Business.Model.User.DTO
{
    public class UserCreateDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public bool Active { get; set; }
        public string? UrlImageProfile { get; set; }
        public string? NumberPhone { get; set; }
    }
}
