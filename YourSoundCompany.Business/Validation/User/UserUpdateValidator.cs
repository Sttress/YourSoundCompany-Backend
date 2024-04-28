using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.RelationalData;

namespace YourSoundCompnay.Business.Validation.User
{
    public class UserUpdateValidator : AbstractValidator<UserCreateModel>
    {
        private readonly IUserRepository _userRepository;

        public UserUpdateValidator(IUserRepository userRepository) 
        {

            _userRepository = userRepository;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome invalido!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email invalido!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalido!")
                     .MustAsync(VerifyEmailEqual)
                     .When(e => !string.IsNullOrWhiteSpace(e.Email))
                     .WithMessage("Email invalido!");
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
