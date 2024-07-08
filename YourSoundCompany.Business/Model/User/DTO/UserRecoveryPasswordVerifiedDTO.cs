using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business.Model.User.DTO
{
    public class UserRecoveryPasswordVerifiedDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Code { get; set; }

    }
}
