using MasterDegree.Dto;
using MasterDegree.Models;

namespace MasterDegree.Interfaces
{
    public interface IDataRepository
    {
        public Task<Product?> GetProductById(int productId);
        public Task<int> GetProductsCount();
        public Task<int> GetProductsCountByParam(string productName);
        public Task<int> GetFavoritesProductsCount(int userId);
        public Task<List<Product>> GetAllProducts();
        public Task<List<Product>> GetFavoritesProductsWithPaging(int userId, PagingHeader pagingHeader);
        public Task<List<Product>> GetProductsWithPaging(PagingHeader pagingHeader);
        public Task<List<Product>> GetProductsWithPagingAndParam(PagingHeader pagingHeader, string productName);
    }
}
