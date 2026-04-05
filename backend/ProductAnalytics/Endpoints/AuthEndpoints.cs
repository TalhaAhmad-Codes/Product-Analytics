using ProductAnalytics.DTOs.AuthDTOs;
using ProductAnalytics.JWT;
using ProductAnalytics.Services.Interfaces;

namespace ProductAnalytics.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/login", Login);
        }

        private static async Task<IResult> Login(
            LoginDto dto,
            IAuthService authService,
            JwtService jwtService)
        {
            var user = await authService.AuthenticateAsync(dto.Username, dto.Password);

            if (user is null)
                return Results.Unauthorized();

            var token = jwtService.GenerateToken(user);

            return Results.Ok(new { token });
        }
    }
}
