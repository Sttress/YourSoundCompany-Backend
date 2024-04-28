using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompnay.Business.Model.User
{
    public class UserLoginResponseModel
    {
        public string? Token { get; set; }
        public UserResponseModel? user { get; set; }

    }
}
