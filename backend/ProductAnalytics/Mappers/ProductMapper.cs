using ProductAnalytics.DTOs.ProductDTOs;
using ProductAnalytics.Models;

namespace ProductAnalytics.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponseDto ToDto(Product product)
            => new()
            {
                Id = product.Id,
                Image = product.Image,
                Name = product.Name,
                Price = product.Price,
                StockStatus = product.StockStatus,
                CreatedAt = product.CreatedAt
            };
    }
}
