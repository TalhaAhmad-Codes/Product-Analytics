using ProductAnalytics.DTOs.LogsDTOs;

namespace ProductAnalytics.Services.Interfaces
{
    public interface ILogsService : IBaseService<LogsResponseDto, LogsCreateDto, LogsFilterDto, LogsUpdateDto>
    {
        Task<bool> ExistsByProductAndSellDateAsync(int productId, DateOnly sellDate);
    }
}
