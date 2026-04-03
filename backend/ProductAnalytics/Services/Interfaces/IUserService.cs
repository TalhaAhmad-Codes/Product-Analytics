using ProductAnalytics.DTOs.UserDTOs;
using ProductAnalytics.DTOs.UserDTOs.UserUpdateDtos;

namespace ProductAnalytics.Services.Interfaces
{
    public interface IUserService : IBaseService<UserResponseDto, UserCreateDto, UserFilterDto>
    {
        Task<bool> ExistsByUsernameAsync(string username);

        Task<UserResponseDto> UpdateProfilePictureAsync(UserUpdateProfilePictureDto dto);
        Task<UserResponseDto> UpdateUsernameAsync(UserUpdateUsernameDto dto);
        Task<UserResponseDto> UpdatePasswordAsync(UserUpdatePasswordDto dto);
    }
}
