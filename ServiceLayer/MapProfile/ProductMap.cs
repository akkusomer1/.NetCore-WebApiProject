using AutoMapper;
using CoreLayer.Dtos;
using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.MapProfile
{
    public class ProductMap:Profile
    {
        public ProductMap()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Product,ProductCreateDto>().ReverseMap();
            CreateMap<Product,ProductUpdateDto>().ReverseMap();
            CreateMap<ProductDto,ProductUpdateDto>().ReverseMap();
        }
    }
}
