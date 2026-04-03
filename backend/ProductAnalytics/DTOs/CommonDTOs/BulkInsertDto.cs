namespace ProductAnalytics.DTOs.CommonDTOs
{
    public sealed class BulkInsertDto<T> where T : class
    {
        public List<T> Items { get; init; } = [];
    }
}
