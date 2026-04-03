using ProductAnalytics.DTOs.CommonDTOs;

namespace ProductAnalytics.DTOs.UserDTOs.UserUpdateDtos
{
    public sealed class UserUpdateProfilePictureDto : BaseDto
    {
        public byte[]? ProfilePic { get; init; }
    }

    public sealed class UserUpdateUsernameDto : BaseDto
    {
        public string Username { get; init; }
    }

    public sealed class UserUpdatePasswordDto : BaseDto
    {
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string ConfirmPassword { get; init; }
    }
}
