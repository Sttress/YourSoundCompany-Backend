using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.Business.Model.User.DTO
{
    public class UserVerificationEmailDTO
    {
        public string? Email { get; set; }
        public int? Code { get; set; }
    }
}
