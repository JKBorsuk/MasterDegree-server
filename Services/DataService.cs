using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Models;
using MasterDegree.Static;

namespace MasterDegree.Services
{
    public class DataService(IDataRepository dataRepository, IUserRepository userRepository) : IDataService
    {
        public async Task<List<Product>> GetAllProducts()
        {
            return await dataRepository.GetAllProducts();
        }

        public async Task<PagingContent<Product>> GetUserFavoritesDataWithPaging(int userId, PagingHeader pagingHeader)
        {
            return new PagingContent<Product>
            {
                Content = await dataRepository.GetFavoritesProductsWithPaging(userId, pagingHeader),
                Count = await dataRepository.GetFavoritesProductsCount(userId),
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<PagingContent<Product>> GetDataWithPaging(PagingHeader pagingHeader)
        {
            return new PagingContent<Product>
            {
                Content = await dataRepository.GetProductsWithPaging(pagingHeader),
                Count = await dataRepository.GetProductsCount(),
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<PagingContent<Product>> GetDataWithPagingAndParam(PagingHeader pagingHeader, string productName)
        {
            return new PagingContent<Product>
            {
                Content = await dataRepository.GetProductsWithPagingAndParam(pagingHeader, productName),
                Count = await dataRepository.GetProductsCountByParam(productName),
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<int> AddFavoriteProduct(int userId, int productId)
        {
            User? user = await userRepository.GetUserById(userId) ?? throw new Exception(ErrorMessage.ERROR_USER_NOT_FOUND);
            Product? product = await dataRepository.GetProductById(productId) ?? throw new Exception(ErrorMessage.ERROR_PRODUCT_NOT_FOUND);
            UserFavorites? userFavorite = await userRepository.GetUserFavorite(userId, productId);

            if(userFavorite != null)
            {
                throw new Exception(ErrorMessage.ERROR_FAVORITE_EXISTS);
            }

            user.Favorites.Add(new UserFavorites()
            {
                User = user,
                Product = product
            });

            return await userRepository.UpdateUser(user);
        }

        public async Task<int> RemoveFavoriteProduct(int userId, int productId)
        {
            User? user = await userRepository.GetUserById(userId) ?? throw new Exception(ErrorMessage.ERROR_USER_NOT_FOUND);
            _ = await dataRepository.GetProductById(productId) ?? throw new Exception(ErrorMessage.ERROR_PRODUCT_NOT_FOUND);
            UserFavorites? userFavorite = await userRepository.GetUserFavorite(userId, productId) ?? throw new Exception(ErrorMessage.ERROR_USER_FAVORITE_NOT_FOUND);

            user.Favorites.Remove(userFavorite);

            return await userRepository.UpdateUser(user);
        }
    }
}
