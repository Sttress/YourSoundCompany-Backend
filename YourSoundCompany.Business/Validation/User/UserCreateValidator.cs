﻿
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using YourSoundCompnay.Business.Model.User;
using YourSoundCompnay.RelationalData;
using YourSoundCompnay.RelationalData.Repository;

namespace YourSoundCompnay.Business.Validation.User
{
    public class UserCreateValidator : AbstractValidator<UserCreateModel>
    {
        private readonly IUserRepository _userRepository;

        public UserCreateValidator(IUserRepository userRepository) 
        {
            _userRepository = userRepository;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome invalido!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email invalido!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email invalido!")
                     .MustAsync(VerifyEmailEqual)
                     .When(e => !string.IsNullOrWhiteSpace(e.Email))
                     .WithMessage("Email invalido!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("A Senha invalida!")
                                    .Matches(@"[a-zA-z]+")
                                    .Matches(@"[0-9]+")
                                    .Matches(@"[\!\?\*\.\@\#\%]+")
                                    .WithMessage("A Senha invalida!");
            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password).WithMessage("A Senhas informadas não coincidem!");

        }
        private async Task<bool> VerifyEmailEqual(string? email, CancellationToken ct)
        {


            var user = await _userRepository.GetUserByEmail(email);
            if (user.Any())
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
