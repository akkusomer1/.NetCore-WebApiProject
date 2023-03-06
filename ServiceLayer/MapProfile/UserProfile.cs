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
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser,RegisterDto>().ReverseMap();
            CreateMap<AppUser, CheckUserResponseDto>().ReverseMap();
        }
    }
}
