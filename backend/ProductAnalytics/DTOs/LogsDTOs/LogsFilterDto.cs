using ProductAnalytics.DTOs.CommonDTOs;

namespace ProductAnalytics.DTOs.LogsDTOs
{
    public sealed class LogsFilterDto : BaseFilterDto
    {
        public int? ProductId { get; init; }
        public int? MinQuantity { get; init; }
        public int? MaxQuantity { get; init; }
        public DateOnly? FromSellDate { get; init; }
        public DateOnly? ToSellDate { get; init; }
    }
}
