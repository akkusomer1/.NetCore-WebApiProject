using CoreLayer.Dtos;
using FluentValidation;

namespace ServiceLayer.Validation
{
    public class ProductUpdateDtoValidotor:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidotor()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
           .MinimumLength(3).WithMessage("{PropertyName}  az 3 karakter olamlıdır.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
        }

    }
}
