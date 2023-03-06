using CoreLayer.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validation
{
    public class ProductCreateDtoValidotor:AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidotor()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.")
                .MinimumLength(3).WithMessage("{PropertyName}  az 3 karakter olamlıdır.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("{PropertyName} boş bırakılamaz.").InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} 0'dan büyük olmalıdır.");
        }

    }
}
