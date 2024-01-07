using FluentValidation;
using System;
using SystemStock.Business.Model.Category;
using SystemStock.RelationalData;

namespace SystemStock.Business.Validation.Category
{
    public class CategoryCreateValidator : AbstractValidator<CategoryModel>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCreateValidator
            (
                ICategoryRepository categoryRepository
            )
        { 
            _categoryRepository = categoryRepository;

            RuleFor(e => e.Name).NotEmpty().WithMessage("Nome inválido")
                                           .MustAsync(VerifyName)
                                           .When(e => !string.IsNullOrWhiteSpace(e.Name))
                                           .WithMessage("Nome inválido");

        }

        private async Task<bool> VerifyName(string? name, CancellationToken ct)
        {
            var category = await _categoryRepository.GetByName(name);
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
