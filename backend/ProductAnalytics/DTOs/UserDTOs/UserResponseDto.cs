using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.UserDTOs
{
    public sealed class UserResponseDto : BaseAuditableDto
    {
        public byte[]? ProfilePic { get; init; }
        public string Username { get; init; }
        public Role Role { get; init; }
    }
}
