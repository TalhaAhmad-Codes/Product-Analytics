using ProductAnalytics.DTOs.UserDTOs;
using ProductAnalytics.Models;

namespace ProductAnalytics.Mappers
{
    public static class UserMapper
    {
        public static UserResponseDto ToDto(User user)
            => new()
            {
                Id = user.Id,
                ProfilePic = user.ProfilePicture,
                Username = user.Username,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
    }
}
