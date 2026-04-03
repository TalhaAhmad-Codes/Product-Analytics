using ProductAnalytics.Enums;
using ProductAnalytics.Utils;

namespace ProductAnalytics.DTOs.UserDTOs
{
    public sealed class UserCreateDto
    {
        private string username;

        public byte[]? ProfilePic { get; init; }
        public string Username
        {
            get => username;
            init => username = Misc.Simplify(value);
        }
        public string Password { get; init; }
        public Role Role { get; init; }
    }
}
