using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompany.Business.Model.User.DTO;

namespace YourSoundCompany.Business.Validation.User
{
    public class UserRecoveryPasswordValidator : AbstractValidator<UserRecoveryPasswordVerifiedDTO>
    {
        public UserRecoveryPasswordValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email invalido!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("A Senha invalida!")
                                    .Matches(@"[a-zA-z]+")
                                    .Matches(@"[0-9]+")
                                    .Matches(@"[\!\?\*\.\@\#\%]+")
                                    .WithMessage("A Senha invalida!");
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password).WithMessage("A Senhas informadas não coincidem!");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Não foi possivel validar a identidade do usúario!");

        }
    }
}
