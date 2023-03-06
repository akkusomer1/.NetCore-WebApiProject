using AutoMapper;
using CoreLayer.Consts;
using CoreLayer.Dtos;
using CoreLayer.Entities;
using CoreLayer.Enums;
using CoreLayer.Interfaces.Repositories;
using CoreLayer.Interfaces.Services;
using CoreLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserService : GenericService<AppUser, AppUserDto>, IUserService
    {
        public UserService(IGenericRepository<AppUser> repo, IUnitOfWork unitOfWork, IMapper mapper) : base(repo, unitOfWork, mapper)
        {
        }

        public  CustomResponseDto<JwtResponseTokenDto> CreateToken(CheckUserResponseDto checkUserResponseDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,checkUserResponseDto.Id.ToString()),
                new Claim(ClaimTypes.Name,checkUserResponseDto.UserName),
                new Claim(ClaimTypes.Role,checkUserResponseDto.RoleName)
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
              issuer: JwtTokenSettings.Issuer,
              audience: JwtTokenSettings.Audience,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddDays(JwtTokenSettings.Expire),
              signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var tokenDto = new JwtResponseTokenDto { Token = handler.WriteToken(jwtSecurityToken), Expires = DateTime.Now.AddDays(JwtTokenSettings.Expire) };
            return CustomResponseDto<JwtResponseTokenDto>.Success(tokenDto, StatusCodes.Status200OK);
        }


   
        public async Task<CustomResponseDto<NoContentDto>> RegisterCreateAsync(RegisterDto registerDto)
        {
            if (registerDto.UserName != null & registerDto.Password != null)
            {
                var user = _mapper.Map<AppUser>(registerDto);
                user.AppRoleId = (int)RoleType.User;

                await _repo.AddAsync(user);
                await _unitOfWork.CommitAsync();
                return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status201Created);
            }
            return CustomResponseDto<NoContentDto>.Fail(null, StatusCodes.Status400BadRequest, "Kullanıcıyı kaydederken bir hata oluştu");
        }

        public async Task<CustomResponseDto<CheckUserResponseDto>> SignInAsync(SignInDto signInDto)
        {
            var hasUser = await _repo.Where(x => x.UserName == signInDto.UserName && x.Password == signInDto.Password).SingleOrDefaultAsync();

            if (hasUser is not null)
            {
                var user = await _repo.GetByIdAsync(hasUser.Id);

                var userResponseDto = _mapper.Map<CheckUserResponseDto>(user);
                var roleName = user.AppRoleId == 1 ? RoleType.Admin.ToString() : RoleType.User.ToString();
                userResponseDto.RoleName = roleName;

                return CustomResponseDto<CheckUserResponseDto>.Success(userResponseDto, StatusCodes.Status200OK);
            }
            return CustomResponseDto<CheckUserResponseDto>.Fail(null, StatusCodes.Status400BadRequest, "Kullanıcı Adı veya şifre yanlış");
        }

      
        public async Task<CustomResponseDto<JwtResponseTokenDto>> SignInByCreateTokenAsync(SignInDto signInDto)
        {
            var hasUser = await _repo.Where(x => x.UserName == signInDto.UserName && x.Password == signInDto.Password).SingleOrDefaultAsync();
            if (hasUser is not null)
            {
                var user = await _repo.GetByIdAsync(hasUser.Id);

                var userResponseDto = _mapper.Map<CheckUserResponseDto>(user);
                var roleName = user.AppRoleId == 1 ? RoleType.Admin.ToString() : RoleType.User.ToString();
                userResponseDto.RoleName = roleName;

                CustomResponseDto<JwtResponseTokenDto> tokenResponse = CreateToken(userResponseDto);

                return tokenResponse;
            }
            return CustomResponseDto<JwtResponseTokenDto>.Fail(null, StatusCodes.Status400BadRequest, "Kullanıcı Adı veya şifre yanlış");
        }
    }
}
