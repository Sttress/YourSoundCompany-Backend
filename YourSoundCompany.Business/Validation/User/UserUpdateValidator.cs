using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Business.Model.User.DTO;
using YourSoundCompnay.RelationalData;

namespace YourSoundCompnay.Business.Validation.User
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        private readonly IUserRepository _userRepository;

        public UserUpdateValidator(IUserRepository userRepository) 
        {

            _userRepository = userRepository;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome invalido!");
        }
        private async Task<bool> VerifyEmailEqual(string? email, CancellationToken ct)
        {


            var user = await _userRepository.GetUserByEmail(email);
            if (user is not null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
