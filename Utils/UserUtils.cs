using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasterDegree.Utils
{
    public static class UserUtils
    {
        public static string ToHash(this string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }
        public static bool VerifyPassword(string password, string? hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public static JwtSecurityToken CreateToken(string? issuer, string? audience, string secretKey, IEnumerable<Claim> claims, bool rememberMe)
        {
            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: rememberMe == true ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
