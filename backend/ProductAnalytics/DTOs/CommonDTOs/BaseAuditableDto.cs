namespace ProductAnalytics.DTOs.CommonDTOs
{
    public abstract class BaseAuditableDto : BaseDto
    {
        public DateTime CreatedAt { get; init; }
    }
}
