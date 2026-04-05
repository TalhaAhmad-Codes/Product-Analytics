using ProductAnalytics.Enums;
using ProductAnalytics.Utils;
using System.ComponentModel;

namespace ProductAnalytics.DTOs.UserDTOs
{
    public sealed class UserCreateDto
    {
        private string username;

        [DefaultValue(null)]
        public byte[]? ProfilePic { get; set; } = null;
        public string Username
        {
            get => username;
            init => username = Misc.Simplify(value, false);
        }
        public string Password { get; init; }
        public Role Role { get; init; }
    }
}
