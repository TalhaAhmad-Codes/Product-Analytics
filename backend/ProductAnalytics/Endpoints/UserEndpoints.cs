using ProductAnalytics.DTOs.UserDTOs;
using ProductAnalytics.DTOs.UserDTOs.UserUpdateDtos;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/users");

            // Endpoints
            group.MapGet("/", GetAllUsersAsync);
            group.MapGet("/{id:int}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPatch("/update/profile-pic", UpdateProfilePictureAsync);
            group.MapPatch("/update/username", UpdateUsernameAsync);
            group.MapPatch("/update/password", UpdatePasswordAsync);

            group.MapDelete("/{id:int}", RemoveAsync);
        }

        /*/ <----- Handlers -----> /*/
        private static async Task<IResult> GetAllUsersAsync([AsParameters] UserFilterDto query, IUserService service)
        {
            try
            {
                var result = await service.GetAllAsync(query);
                return Results.Ok(result);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> GetByIdAsync(int id, IUserService service)
        {
            var user = await service.GetByIdAsync(id);
            return user is null ? Results.NotFound() : Results.Ok(user);
        }

        private static async Task<IResult> CreateAsync(UserCreateDto dto, IUserService service)
        {
            try
            {
                var user = await service.CreateAsync(dto);
                return Results.Created($"/api/users/{user.Id}", user);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> UpdateProfilePictureAsync(UserUpdateProfilePictureDto dto, IUserService service)
        {
            try
            {
                var user = await service.UpdateProfilePictureAsync(dto);
                return Results.Ok(user);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> UpdateUsernameAsync(UserUpdateUsernameDto dto, IUserService service)
        {
            try
            {
                var user = await service.UpdateUsernameAsync(dto);
                return Results.Ok(user);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> UpdatePasswordAsync(UserUpdatePasswordDto dto, IUserService service)
        {
            try
            {
                var user = await service.UpdatePasswordAsync(dto);
                return Results.Ok(user);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> RemoveAsync(int id, IUserService service)
        {
            var success = await service.RemoveAsync(id);
            return success ? Results.Ok("User has been removed successfully.") : Results.NotFound();
        }
    }
}
