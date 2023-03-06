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
    public class ProductFeatureMap:Profile
    {
        public ProductFeatureMap()
        {
            CreateMap<ProductFeature, ProductFeatureDto>();
        }
    }
}
