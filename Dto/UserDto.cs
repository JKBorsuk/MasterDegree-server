using MasterDegree.Models;

namespace MasterDegree.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public List<Product> Favorites { get; set; } = [];
    }
}
