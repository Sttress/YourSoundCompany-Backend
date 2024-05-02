using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business.Model.User
{
    public class UserVerificationEmail
    {
        public string Email { get; set; }
        public int? Code { get; set; }
    }
}
