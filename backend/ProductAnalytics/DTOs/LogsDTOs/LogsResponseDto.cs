using ProductAnalytics.DTOs.CommonDTOs;

namespace ProductAnalytics.DTOs.LogsDTOs
{
    public sealed class LogsResponseDto : BaseAuditableDto
    {
        public int ProductId { get; init; }
        public DateOnly SellDate { get; init; }
        public int Quantity { get; init; }
    }
}
