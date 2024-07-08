using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Business.Model.User.DTO;

namespace YourSoundCompnay.Business.Model.User
{
    public class UserLoginResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public UserResponseDTO? user { get; set; }

    }
}
