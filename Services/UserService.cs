using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Mapper;
using MasterDegree.Models;
using MasterDegree.Static;
using MasterDegree.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MasterDegree.Services
{
    public class UserService(IUserRepository userRepository, IConfiguration configuration) : IUserService
    {
        public async Task<UserDto?> GetUserById(int id)
        {
            User? user = await userRepository.GetUserById(id) ?? throw new Exception(ErrorMessage.ERROR_USER_NOT_FOUND);
            return user.ToUserDto();
        }

        public async Task<int> RegisterUser(AuthenticateUserDto userData)
        {
            if(await userRepository.UserExists(userData.Email))
            {
                throw new Exception(ErrorMessage.ERROR_USER_EXISTS);
            }

            if(!UserUtils.VerifyEmail(userData.Email))
            {
                throw new Exception(ErrorMessage.ERROR_EMAIL_DATA);
            }

            if(userData.Password.Length < 3)
            {
                throw new Exception(ErrorMessage.ERROR_PASSWORD_DATA);
            }

            User user = new()
            {
                Email = userData.Email,
                Password = userData.Password.ToHash()
            };

            return await userRepository.CreateUser(user);
        }

        public async Task<AuthorizeUserDto> LoginUser(AuthenticateUserDto userData)
        {
            User? user = await userRepository.GetUserByEmail(userData.Email) ?? throw new Exception(ErrorMessage.ERROR_USER_NOT_FOUND_BY_EMAIL);

            if (!UserUtils.VerifyPassword(userData.Password, user.Password)) {
                throw new Exception(ErrorMessage.ERROR_USER_PASSWORD_INCORRECT);
            }

            JwtSecurityToken? token = GenerateAccessToken(user);

            return new()
            {
                User = user.ToUserDto(),
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public JwtSecurityToken GenerateAccessToken(User user, bool rememberMe = false)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return UserUtils.CreateToken(
                configuration["JwtSettings:Issuer"],
                configuration["JwtSettings:Audience"],
                configuration["JwtSettings:SecretKey"]!,
                claims
            );
        }
    }
}
