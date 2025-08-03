using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDegree.Repository
{
    public class DataRepository(MasterDegreeDbContext masterDegreeDbContext) : IDataRepository
    {
        public async Task<List<Product>> GetAllProducts()
        {
            return await masterDegreeDbContext.Products
                .ToListAsync();
        }
        public async Task<int> GetFavoritesProductsCount(int userId)
        {
            return await masterDegreeDbContext.UserFavorites
                .Where(x => x.UserId == userId)
                .CountAsync();
        }

        public async Task<int> GetProductsCountByParam(string productName)
        {
            return await masterDegreeDbContext.Products
                .Where(x => x.ProductName == productName)
                .CountAsync();
        }

        public async Task<List<Product>> GetFavoritesProductsWithPaging(int userId, PagingHeader pagingHeader)
        {
            return await masterDegreeDbContext.UserFavorites
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsWithPaging(PagingHeader pagingHeader)
        {
            return await masterDegreeDbContext.Products
                .Skip(pagingHeader.PageIndex * pagingHeader.PageSize)
                .Take(pagingHeader.PageSize)
                .ToListAsync();
        }
        public async Task<int> GetProductsCount()
        {
            return await masterDegreeDbContext.Products
                .CountAsync();
        }

        public async Task<List<Product>> GetProductsWithPagingAndParam(PagingHeader pagingHeader, string productName)
        {
            return await masterDegreeDbContext.Products
                .Where(x => x.ProductName == productName)
                .Skip(pagingHeader.PageIndex * pagingHeader.PageSize)
                .Take(pagingHeader.PageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await masterDegreeDbContext.Products
                .FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
