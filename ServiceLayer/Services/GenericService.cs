using AutoMapper;
using CoreLayer.Dtos;
using CoreLayer.Entities;
using CoreLayer.Interfaces.Repositories;
using CoreLayer.Interfaces.Services;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : BaseEntity, new()
    {
        protected readonly IGenericRepository<TEntity> _repo;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<NoContentDto>> AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await _repo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> AddRAngeAsync(IEnumerable<TDto> dtos)
        {
            IEnumerable<TEntity> entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _repo.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            var newdtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(newdtos, 200);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> pridicate)
        {
            bool anyEntity = await _repo.AnyAsync(pridicate);
            return CustomResponseDto<bool>.Success(anyEntity, 200);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var listEntity = await _repo.GetAll().ToListAsync();
            var listDto = _mapper.Map<IEnumerable<TDto>>(listEntity);
            return CustomResponseDto<IEnumerable<TDto>>.Success(listDto, StatusCodes.Status200OK);

        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            TEntity entity = await _repo.GetByIdAsync(id);
            TDto dto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(dto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            TEntity entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(null,404,"data yok.");
            }

            _repo.Remove(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRange(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            _repo.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repo.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

        }

        public CustomResponseDto<IEnumerable<TDto>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var listEntity = _repo.Where(predicate).ToListAsync();
            var listDto = _mapper.Map<IEnumerable<TDto>>(listEntity);
            return CustomResponseDto<IEnumerable<TDto>>.Success(listDto, StatusCodes.Status200OK);
        }
    }
}
