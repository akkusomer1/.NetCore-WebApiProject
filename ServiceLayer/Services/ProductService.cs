using AutoMapper;
using CoreLayer.Dtos;
using CoreLayer.Entities;
using CoreLayer.Interfaces.Repositories;
using CoreLayer.Interfaces.Services;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductService : GenericService<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IGenericRepository<Product> repo, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repo, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            Product savedProduct = await _repo.AddAsync(product);

            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDto>(savedProduct);
            return CustomResponseDto<ProductDto>.Success(productDto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetProductWithCategoryAsync()
        {
            List<Product> producsWithCategory = await _productRepository.GetProductWithCategoryAsync();
            List<ProductDto> listDto = _mapper.Map<List<ProductDto>>(producsWithCategory);
            return CustomResponseDto<List<ProductDto>>.Success(listDto, 200);

        }

        public async Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var product = _mapper.Map<Product>(productUpdateDto);
            _repo.Update(product);
            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDto>(productUpdateDto);
            return CustomResponseDto<ProductDto>.Success(productDto, StatusCodes.Status200OK);
        }
    }
}
