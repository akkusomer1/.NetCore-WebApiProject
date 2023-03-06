using CoreLayer.Dtos;
using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Services
{
    public interface IUserService:IGenericService<AppUser,AppUserDto>
    {
        Task<CustomResponseDto<NoContentDto>> RegisterCreateAsync(RegisterDto registerDto);
        Task<CustomResponseDto<CheckUserResponseDto>> SignInAsync(SignInDto signInDto);
        CustomResponseDto<JwtResponseTokenDto> CreateToken(CheckUserResponseDto checkUserResponseDto);
        Task<CustomResponseDto<JwtResponseTokenDto>> SignInByCreateTokenAsync(SignInDto signInDto);
    }
}
