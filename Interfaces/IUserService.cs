using MasterDegree.Dto;
using MasterDegree.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MasterDegree.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto?> GetUserById(int id);
        public Task<int> RegisterUser(AuthenticateUserDto userData);
        public Task<AuthorizeUserDto> LoginUser(AuthenticateUserDto userData);
        public JwtSecurityToken GenerateAccessToken(User user, bool rememberMe = false);
    }
}
