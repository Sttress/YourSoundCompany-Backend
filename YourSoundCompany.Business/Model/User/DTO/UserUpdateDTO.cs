using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business.Model.User.DTO
{
    public class UserUpdateDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? UrlImageProfile { get; set; }
    }
}
