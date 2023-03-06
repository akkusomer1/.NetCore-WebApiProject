using CoreLayer.Dtos;
using CoreLayer.Entities;
using CoreLayer.Interfaces.Services;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return CreateActionResult(await _userService.RegisterCreateAsync(registerDto));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
           return CreateActionResult(await _userService.SignInAsync(signInDto));
        }
         
        
        [HttpPost("[action]")]
        public async Task<IActionResult> SignInByCreateToken(SignInDto signInDto)
        {
           return CreateActionResult(await _userService.SignInByCreateTokenAsync(signInDto));
        }
    }
}
