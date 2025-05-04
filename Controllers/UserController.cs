using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Static;
using MasterDegree.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MasterDegree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await ExecuteUtil<UserDto>.Execute(async () => await userService.GetUserById(id)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticateUserDto userData)
        {
            return Ok(await ExecuteUtil<int>.Execute(async () => await userService.RegisterUser(userData)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateUserDto userData)
        {
            return Ok(await ExecuteUtil<AuthorizeUserDto>.Execute(async () => await userService.LoginUser(userData)));
        }

        [HttpGet("authorize")]
        public async Task<IActionResult> Authorize()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if(userClaim == null)
            {
                return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
            }

            if(int.TryParse(userClaim.Value, out int userId)) {
                return Ok(await ExecuteUtil<UserDto>.Execute(async () => await userService.GetUserById(userId)));
            }

            return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
        }
    }
}
