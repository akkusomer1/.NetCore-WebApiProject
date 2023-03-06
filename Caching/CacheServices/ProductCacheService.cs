using AutoMapper;
using Caching.ICacheServices;
using Caching.RedisServices;
using CoreLayer.Dtos;
using CoreLayer.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Text.Json;

namespace Caching.CacheServices
{
    public class ProductCacheService : IProductCacheService
    {
        private string key = "productCaches";
        private readonly IProductService _productService;
        private readonly RedisService _redisService;
        private readonly IDatabase _db;

        public ProductCacheService(RedisService redisService, IProductService productService)
        {
            _redisService = redisService;
            _db = _redisService.GetDatabase(1);
            _productService = productService;
        }

        public async Task<CustomResponseDto<List<ProductDto>>> GetAllAsync()
        {
            if (!await _db.KeyExistsAsync(key))
            {
                return await LoadCacheFromDbAsync();
            }

            var listCacheProduct = await _db.HashGetAllAsync(key);

            List<ProductDto> productDtos = new List<ProductDto>();
            listCacheProduct.ToList().ForEach(p =>
            {
                var productDto = JsonSerializer.Deserialize<ProductDto>(p.Value!);
                productDtos.Add(productDto);
            });
            return CustomResponseDto<List<ProductDto>>.Success(productDtos, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<ProductDto>> GetByIdAsync(int id)
        {
            if (await _db.KeyExistsAsync(key))
            {
                var jsonData = await _db.HashGetAsync(key, id);
                if (jsonData.HasValue)
                {
                    var productDto = JsonSerializer.Deserialize<ProductDto>(jsonData);
                    return CustomResponseDto<ProductDto>.Success(productDto, StatusCodes.Status200OK);
                }
            }
            var products = await LoadCacheFromDbAsync();
            var product = products.Data.FirstOrDefault(x => x.Id == id);
            return CustomResponseDto<ProductDto>.Success(product, StatusCodes.Status200OK);
        }


        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var product = await _productService.AddAsync(productCreateDto);
            if (_db.KeyExists(key))
            {
                await _db.HashSetAsync(key, product.Data.Id, JsonSerializer.Serialize(product.Data));
            }
            return product;
        }

        public async Task<CustomResponseDto<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var product = await _productService.UpdateAsync(productUpdateDto);
            if (await _db.KeyExistsAsync(key))
            {
                _db.HashDelete(key, product.Data.Id);
                await _db.HashSetAsync(key, product.Data.Id, JsonSerializer.Serialize(product.Data));
            }
            return product;
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            if (_db.KeyExists(key))
            {
                await _db.HashDeleteAsync(key, id);
            }
            return await _productService.RemoveAsync(id);
        }

        //Tüm product'ları cache yükler.
        private async Task<CustomResponseDto<List<ProductDto>>> LoadCacheFromDbAsync()
        {
            var products = await _productService.GetProductWithCategoryAsync();

            products.Data.ToList().ForEach(async x =>
            {
                var jsonProduct = JsonSerializer.Serialize(x);
                await _db.HashSetAsync(key, x.Id, jsonProduct);
            });
            return products;
        }

    }
}
