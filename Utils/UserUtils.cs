using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
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
        public static JwtSecurityToken CreateToken(string? issuer, string? audience, string secretKey, IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256)
            );
        }
        public static bool VerifyEmail(string? email)
        {
            if (email is null || email.Length == 0)
            {
                return false;
            }
            try
            {
                MailAddress m = new(email);
                return email.Length <= 100;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
