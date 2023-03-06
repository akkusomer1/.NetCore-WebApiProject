using CoreLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.ICacheServices
{
    public interface IProductCacheService
    {
        Task<CustomResponseDto<List<ProductDto>>> GetAllAsync();
        Task<CustomResponseDto<ProductDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
        Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

    }
}
