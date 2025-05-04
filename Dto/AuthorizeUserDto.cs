using MasterDegree.Models;

namespace MasterDegree.Dto
{
    public class AuthorizeUserDto
    {
        public required UserDto User { get; set; }
        public required string AccessToken { get; set; }
    }
}
