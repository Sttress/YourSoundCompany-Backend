using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Business.Model.User.DTO;

namespace YourSoundCompany.Business.Model.Authentication.DTO
{
    public class AuthLoginResponseDTO
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public UserResponseDTO? user { get; set; }

    }
}
