using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Models;
using MasterDegree.Static;

namespace MasterDegree.Services
{
    public class DataService(IDataRepository dataRepository, IUserRepository userRepository) : IDataService
    {
        public async Task<PagingContent<Product>> GetUserFavoritesDataWithPaging(int userId, PagingHeader pagingHeader)
        {
            List<Product> products = await dataRepository.GetFavoritesProductsWithPaging(userId, pagingHeader);
            int productsCount = await dataRepository.GetFavoritesProductsCount(userId);
            return new PagingContent<Product>
            {
                Content = products,
                Count = productsCount,
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<PagingContent<Product>> GetDataWithPaging(PagingHeader pagingHeader)
        {
            List<Product> products = await dataRepository.GetProductsWithPaging(pagingHeader);
            int productsCount = await dataRepository.GetProductsCount();
            return new PagingContent<Product>
            {
                Content = products,
                Count = productsCount,
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<PagingContent<Product>> GetDataWithPagingAndParam(PagingHeader pagingHeader, string productName)
        {
            List<Product> products = await dataRepository.GetProductsWithPagingAndParam(pagingHeader, productName);
            int productsCount = await dataRepository.GetProductsCountByParam(productName);
            return new PagingContent<Product>
            {
                Content = products,
                Count = productsCount,
                PageIndex = pagingHeader.PageIndex
            };
        }

        public async Task<int> AddFavoriteProduct(int userId, int productId)
        {
            User? user = await userRepository.GetUserById(userId) ?? throw new Exception(ErrorMessage.ERROR_USER_NOT_FOUND);
            Product? product = await dataRepository.GetProductById(productId) ?? throw new Exception(ErrorMessage.ERROR_PRODUCT_NOT_FOUND);
            UserFavorites? userFavorite = await userRepository.GetUserFavorite(userId, productId);

            if(userFavorite == null)
            {
                user.Favorites.Add(new UserFavorites()
                {
                    User = user,
                    Product = product
                });

                return await userRepository.UpdateUser(user);
            }

            return -1;
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
