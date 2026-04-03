using ProductAnalytics.DTOs.LogsDTOs;
using ProductAnalytics.Models;

namespace ProductAnalytics.Mappers
{
    public static class LogsMapper
    {
        public static LogsResponseDto ToDto(Logs logs)
            => new()
            {
                Id = logs.Id,
                ProductId = logs.ProductId,
                SellDate = logs.SellDate,
                Quantity = logs.Quantity,
                CreatedAt = logs.CreatedAt
            };
    }
}
