using FluentValidation;
using SystemStock.Business.Model.Product;
using SystemStock.Business.Service;
using SystemStock.RelationalData;
using SystemStock.RelationalData.Repository;

namespace SystemStock.Business.Validation.Product
{
    public class ProductCreateValidator :AbstractValidator<ProductModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        public ProductCreateValidator(IProductRepository productRepository, IUserService userService) 
        {

            _productRepository = productRepository;
            _userService = userService;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome inválido")
                                .MustAsync(VerifyName)
                                .When(e => !string.IsNullOrWhiteSpace(e.Name))
                                .WithMessage("Nome inválido1");
        }

        private async Task<bool> VerifyName(string? name, CancellationToken ct)
        {
            var userId = (await _userService.GetCurrentUser()).Id;

            var product = await _productRepository.GetByName(name,userId);

            if (product is null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
