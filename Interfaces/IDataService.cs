using MasterDegree.Dto;
using MasterDegree.Models;

namespace MasterDegree.Interfaces
{
    public interface IDataService
    {
        public Task<int> AddFavoriteProduct(int userId, int productId);
        public Task<int> RemoveFavoriteProduct(int userId, int productId);
        public Task<PagingContent<Product>> GetUserFavoritesDataWithPaging(int userId, PagingHeader pagingHeader);
        public Task<PagingContent<Product>> GetDataWithPaging(PagingHeader pagingHeader);
        public Task<PagingContent<Product>> GetDataWithPagingAndParam(PagingHeader pagingHeader, string productName);
    }
}
