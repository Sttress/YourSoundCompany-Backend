using FluentValidation;
using System;
using SystemStock.Business.Model.Category;
using SystemStock.Business.Service;
using SystemStock.RelationalData;
using SystemStock.SesseionService;

namespace SystemStock.Business.Validation.Category
{
    public class CategoryCreateValidator : AbstractValidator<CategoryModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserService _userService;

        public CategoryCreateValidator
            (
                ICategoryRepository categoryRepository,
                IUserService userService
            )
        { 
            _categoryRepository = categoryRepository;
            _userService = userService;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome inválido")
                                           .MustAsync(VerifyName)
                                           .When(e => !string.IsNullOrWhiteSpace(e.Name))
                                           .WithMessage("Nome inválido");

        }

        private async Task<bool> VerifyName(string? name, CancellationToken ct)
        {
            var userId = (await _userService.GetCurrentUser()).Id;
            var category = await _categoryRepository.GetByName(name,userId);
            if(name is null)
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
