﻿
namespace YourSoundCompnay.Business.Model.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public string? UrlImageProfile { get; set; }
        public string? NumberPhone { get; set; }

    }
}
