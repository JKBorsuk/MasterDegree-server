using MasterDegree.Models;

namespace MasterDegree.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(int id);
        public Task<User?> GetUserByEmail(string email);
        public Task<UserFavorites?> GetUserFavorite(int userId, int productId);
        public Task<bool> UserExists(string email);
        public Task<int> CreateUser(User user);
        public Task<int> UpdateUser(User user);
    }
}
