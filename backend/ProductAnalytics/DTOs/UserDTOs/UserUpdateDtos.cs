using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.Utils;

namespace ProductAnalytics.DTOs.UserDTOs.UserUpdateDtos
{
    public sealed class UserUpdateProfilePictureDto : BaseDto
    {
        public byte[]? ProfilePic { get; init; }
    }

    public sealed class UserUpdateUsernameDto : BaseDto
    {
        private string username;
        public string Username
        {
            get => username;
            init => username = Misc.Simplify(value);
        }
    }

    public sealed class UserUpdatePasswordDto : BaseDto
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string ConfirmPassword { get; init; }
    }
}
