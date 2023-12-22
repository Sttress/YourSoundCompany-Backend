using FluentValidation;
using SystemStock.Business.Model.Store;
using SystemStock.Business.Service;
using SystemStock.RelationalData;

namespace SystemStock.Business.Validation.Store
{
    public class StoreCreateValidator : AbstractValidator<StoreModel>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserService _userService;
        public StoreCreateValidator
            (
                IStoreRepository storeRepository,
                IUserService userService
            ) 
        {
            _storeRepository = storeRepository;
            _userService = userService;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome inválido")
                                .MustAsync(VerifyName)
                                .When(e => !string.IsNullOrWhiteSpace(e.Name))
                                .WithMessage("Nome inválido");
            RuleFor(e => e.UserId).MustAsync(VerifyUser)
                                  .When(e => e.UserId > 0)
                                  .WithMessage("Usúario inválido");
        }
        
        private async Task<bool> VerifyName(string? name, CancellationToken ct)
        {
            var store = await _storeRepository.GetByName(name);
            if(name is  null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> VerifyUser(long userId, CancellationToken ct)
        {
            var user = await _userService.VerifyUserById(userId);
            if(user is not true)
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
