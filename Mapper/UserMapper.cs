using MasterDegree.Dto;
using MasterDegree.Models;

namespace MasterDegree.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                Favorites = user.Favorites.Select(x => x.Product).ToList()
            };
        }
    }
}
