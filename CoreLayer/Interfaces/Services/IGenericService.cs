using CoreLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Services
{
    public interface IGenericService<TEntity, TDto>
    {
        Task<CustomResponseDto<TDto>> GetByIdAsync(int id);
        CustomResponseDto<IEnumerable<TDto>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> pridicate);

        Task<CustomResponseDto<NoContentDto>> AddAsync(TDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);

        Task<CustomResponseDto<IEnumerable<TDto>>> AddRAngeAsync(IEnumerable<TDto> dtos);
        Task<CustomResponseDto<NoContentDto>> RemoveRange(IEnumerable<TDto> dtos);

    }
}
