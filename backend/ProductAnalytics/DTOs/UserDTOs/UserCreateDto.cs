using ProductAnalytics.Enums;

namespace ProductAnalytics.DTOs.UserDTOs
{
    public sealed class UserCreateDto
    {
        public byte[]? ProfilePic { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public Role Role { get; init; }
    }
}
