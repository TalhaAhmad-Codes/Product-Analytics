using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Data;
using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.DTOs.UserDTOs;
using ProductAnalytics.DTOs.UserDTOs.UserUpdateDtos;
using ProductAnalytics.Mappers;
using ProductAnalytics.Models;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Services.Implementation
{
    public sealed class UserService : IUserService
    {
        private readonly ProductAnalyticsDbContext context;

        public UserService(ProductAnalyticsDbContext context)
        {
            this.context = context;
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            bool alreadyExists = await ExistsByUsernameAsync(dto.Username);

            if (alreadyExists)
            {
                throw new DomainException($"User of name '{dto.Username}' is already registered.");
            }

            // User creation
            User user = new()
            {
                ProfilePicture = dto.ProfilePic,
                Username = dto.Username,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Role = dto.Role
            };

            // Adding to the database
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            // Returning DTO
            return UserMapper.ToDto(user);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            int count = await context.Users.AsQueryable()
                .Where(u => u.Username == username)
                .CountAsync();

            return count > 0;
        }

        public async Task<PagedResultDto<UserResponseDto>> GetAllAsync(UserFilterDto filterDto)
        {
            var query = context.Users.AsQueryable();

            // Applying Filters
            if (filterDto.UserName != null)
                query = query.Where(u => u.Username == filterDto.UserName);

            if (filterDto.Role.HasValue)
                query = query.Where(u => u.Role == filterDto.Role);

            // Getting paged result
            var totalCount = await query.CountAsync();
            var items = await Misc.GetPagedResultAsync<User>(query, filterDto.PageNumber, filterDto.PageSize);

            // Returning the result
            return new PagedResultDto<UserResponseDto>
            {
                Items = [.. items.Select(UserMapper.ToDto)],
                TotalCount = totalCount
            };
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            return user is null ? null : UserMapper.ToDto(user);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var user = await context.Users.FindAsync(id);

            // User not found!
            if (user is null)
                return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<UserResponseDto> UpdatePasswordAsync(UserUpdatePasswordDto dto)
        {
            var user = await context.Users.FindAsync(dto.Id)
                ?? throw new DomainException("User not found!");

            // Password mismatch
            if (!PasswordHasher.Verify(dto.OldPassword, user.PasswordHash))
                throw new DomainException("Incorrect Password!");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new DomainException("Password Mismatch!");

            // Update the password
            user.PasswordHash = PasswordHasher.Hash(dto.NewPassword);

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return UserMapper.ToDto(user);
        }

        public async Task<UserResponseDto> UpdateProfilePictureAsync(UserUpdateProfilePictureDto dto)
        {
            var user = await context.Users.FindAsync(dto.Id)
                ?? throw new DomainException("User not found!");

            // Update the picture
            user.ProfilePicture = dto.ProfilePic;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return UserMapper.ToDto(user);
        }

        public async Task<UserResponseDto> UpdateUsernameAsync(UserUpdateUsernameDto dto)
        {
            var user = await context.Users.FindAsync(dto.Id)
                ?? throw new DomainException("User not found!");

            // Update the username
            user.Username = dto.Username;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return UserMapper.ToDto(user);
        }
    }
}
