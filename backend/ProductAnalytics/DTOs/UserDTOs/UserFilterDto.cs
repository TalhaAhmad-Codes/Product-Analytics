using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.UserDTOs
{
    public sealed class UserFilterDto : BaseFilterDto
    {
        public string? UserName { get; init; }
        public Role? Role { get; init; }
    }
}
