using MasterDegree.Interfaces;
using MasterDegree.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDegree.Repository
{
    public class UserRepository(MasterDegreeDbContext masterDegreeDbContext) : IUserRepository
    {
        public async Task<int> CreateUser(User user)
        {
            masterDegreeDbContext.Users.Add(user);
            return await masterDegreeDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateUser(User user)
        {
            masterDegreeDbContext.Users.Update(user);
            return await masterDegreeDbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await masterDegreeDbContext.Users
                .Include(x => x.Favorites)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await masterDegreeDbContext.Users
                .Include(x => x.Favorites)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UserExists(string email)
        {
            return await masterDegreeDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email) != null;
        }

        public async Task<UserFavorites?> GetUserFavorite(int userId, int productId)
        {
            return await masterDegreeDbContext.UserFavorites
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
        }
    }
}
