using ProductAnalytics.Enums;
using ProductAnalytics.Models.Common;

namespace ProductAnalytics.Models
{
    public sealed class User : BaseEntity
    {
        public byte[]? ProfilePicture { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
