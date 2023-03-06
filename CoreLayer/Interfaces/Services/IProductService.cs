using CoreLayer.Dtos;
using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Services
{
    public interface IProductService:IGenericService<Product,ProductDto>
    {
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
        Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<CustomResponseDto<List<ProductDto>>> GetProductWithCategoryAsync();
    }
}
