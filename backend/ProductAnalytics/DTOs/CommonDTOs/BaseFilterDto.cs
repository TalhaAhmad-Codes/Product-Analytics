namespace ProductAnalytics.DTOs.CommonDTOs
{
    public abstract class BaseFilterDto
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public void NormalizePagination()
        {
            PageNumber ??= 1;
            PageSize ??= 10;

            if (PageNumber <= 0) PageNumber = 1;
            if (PageSize <= 0) PageSize = 10;
        }
    }
}
