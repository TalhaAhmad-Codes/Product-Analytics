using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Data;
using ProductAnalytics.Data.LengthLimits;
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
            // Guard against invalid data
            Guard.AgainstNullOrWhitespace(dto.Username, "Username");
            Guard.AgainstInvalidMinimumLength(dto.Password, Minimum.Password, "Password");
            Guard.AgainstInvalidRange(0, 1, (int) dto.Role, "Role");

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
                .Where(u => u.Username.ToLower() == username.ToLower())
                .CountAsync();

            return count > 0;
        }

        public async Task<PagedResultDto<UserResponseDto>> GetAllAsync(UserFilterDto filterDto)
        {
            var query = context.Users.AsQueryable();
            filterDto.NormalizePagination();    // Remove nulls

            // Applying Filters
            if (filterDto.Username != null)
            {
                Guard.AgainstNullOrWhitespace(filterDto.Username, "Username");
                query = query.Where(u => u.Username.ToLower() == filterDto.Username.ToLower());
            }

            if (filterDto.Role.HasValue)
            {
                Guard.AgainstInvalidRange(0, 1, (int)filterDto.Role, "Role");
                query = query.Where(u => u.Role == filterDto.Role);
            }

            // Getting paged result
            var totalCount = await query.CountAsync();
            var items = await Misc.GetPagedResultAsync<User>(query, filterDto.PageNumber!.Value, filterDto.PageSize!.Value);

            // Returning the result
            return new PagedResultDto<UserResponseDto>
            {
                Items = [.. items.Select(UserMapper.ToDto)],
                TotalCount = totalCount
            };
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            Guard.AgainstZeroOrLess(id, "Id");

            var user = await context.Users.FindAsync(id);
            return user is null ? null : UserMapper.ToDto(user);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Guard.AgainstZeroOrLess(id, "Id");

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
            // Guard against invalid data
            Guard.AgainstZeroOrLess(dto.Id, "Id");
            Guard.AgainstInvalidMinimumLength(dto.OldPassword, Minimum.Password, "Password (Old)");
            Guard.AgainstInvalidMinimumLength(dto.NewPassword, Minimum.Password, "Password (New)");
            Guard.AgainstInvalidMinimumLength(dto.ConfirmPassword, Minimum.Password, "Password (Confirm)");

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
            Guard.AgainstZeroOrLess(dto.Id, "Id");
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
            // Guard against invalid data
            Guard.AgainstZeroOrLess(dto.Id, "Id");
            Guard.AgainstNullOrWhitespace(dto.Username, "Username");

            var user = await context.Users.FindAsync(dto.Id)
                ?? throw new DomainException("User not found!");

            // Check uniqueness
            bool exists = await ExistsByUsernameAsync(dto.Username);

            if (exists)
                throw new DomainException("A user of same name is already registered.");

            // Update the username
            user.Username = dto.Username;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return UserMapper.ToDto(user);
        }
    }
}
