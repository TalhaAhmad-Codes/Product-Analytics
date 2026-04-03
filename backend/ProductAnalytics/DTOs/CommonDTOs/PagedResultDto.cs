namespace ProductAnalytics.DTOs.CommonDTOs
{
    public sealed class PagedResultDto<T> where T : class
    {
        public List<T> Items { get; init; } = new();
        public int TotalCount { get; init; }
    }
}
