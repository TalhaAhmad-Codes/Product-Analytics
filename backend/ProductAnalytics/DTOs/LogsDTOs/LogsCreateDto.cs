namespace ProductAnalytics.DTOs.LogsDTOs
{
    public sealed class LogsCreateDto
    {
        public int ProductId { get; init; }
        public DateOnly SellDate { get; init; }
        public int Quantity { get; init; }
    }
}
