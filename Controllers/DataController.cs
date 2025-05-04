using MasterDegree.Dto;
using MasterDegree.Interfaces;
using MasterDegree.Models;
using MasterDegree.Static;
using MasterDegree.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MasterDegree.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController(IDataService dataService) : Controller
    {
        [Authorize]
        [HttpPost("user-favorites")]
        public async Task<IActionResult> GetUserFavoritesDataPagingAsync(PagingHeader pagingHeader)
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userClaim == null)
            {
                return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
            }

            if (int.TryParse(userClaim.Value, out int userId))
            {
                return Ok(await ExecuteUtil<PagingContent<Product>>.Execute(async () => await dataService.GetUserFavoritesDataWithPaging(userId, pagingHeader)));
            }

            return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
        }

        [Authorize]
        [HttpPost("add-favorite")]
        public async Task<IActionResult> AddFavoriteProduct(FavoriteProductDto favoriteProductDto)
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userClaim == null)
            {
                return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
            }

            if (int.TryParse(userClaim.Value, out int userId))
            {
                return Ok(await ExecuteUtil<int>.Execute(async () => await dataService.AddFavoriteProduct(userId, favoriteProductDto.ProductId)));
            }

            return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
        }

        [Authorize]
        [HttpPost("remove-favorite")]
        public async Task<IActionResult> RemoveFavoriteProduct(FavoriteProductDto favoriteProductDto)
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userClaim == null)
            {
                return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
            }

            if (int.TryParse(userClaim.Value, out int userId))
            {
                return Ok(await ExecuteUtil<int>.Execute(async () => await dataService.RemoveFavoriteProduct(userId, favoriteProductDto.ProductId)));
            }

            return Ok(new Response<string>() { Success = false, Data = ErrorMessage.ERROR_DATA });
        }

        [HttpPost("data-paging")]
        public async Task<IActionResult> GetDataPagingAsync(PagingHeader pagingHeader)
        {
            return Ok(await ExecuteUtil<PagingContent<Product>>.Execute(async () => await dataService.GetDataWithPaging(pagingHeader)));
        }
        [HttpPost("data-paging/monitors")]
        public async Task<IActionResult> GetDataPagingMonitorsAsync(PagingHeader pagingHeader)
        {
            return Ok(await ExecuteUtil<PagingContent<Product>>.Execute(async () => await dataService.GetDataWithPagingAndParam(pagingHeader, "Monitor")));
        }
        [HttpPost("data-paging/laptops")]
        public async Task<IActionResult> GetDataPagingLaptopsAsync(PagingHeader pagingHeader)
        {
            return Ok(await ExecuteUtil<PagingContent<Product>>.Execute(async () => await dataService.GetDataWithPagingAndParam(pagingHeader, "Laptop")));
        }
        [HttpPost("data-paging/headphones")]
        public async Task<IActionResult> GetDataPagingHeadphonesAsync(PagingHeader pagingHeader)
        {
            return Ok(await ExecuteUtil<PagingContent<Product>>.Execute(async () => await dataService.GetDataWithPagingAndParam(pagingHeader, "Headphones")));
        }
    }
}
