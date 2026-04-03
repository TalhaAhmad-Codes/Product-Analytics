namespace ProductAnalytics.DTOs.CommonDTOs
{
    public abstract class BaseFilterDto
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
