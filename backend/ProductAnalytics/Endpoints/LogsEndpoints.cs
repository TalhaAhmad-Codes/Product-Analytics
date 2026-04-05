using ProductAnalytics.DTOs.LogsDTOs;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Endpoints
{
    public static class LogsEndpoints
    {
        public static void MapLogsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/logs");

            // Endpoints
            group.MapGet("/", GetAllAsync);
            group.MapGet("/{id:int}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPut("/", UpdateAsync);

            group.MapDelete("/{id:int}", RemoveAsync);
        }

        /*/ <----- Handlers -----> /*/
        private static async Task<IResult> GetAllAsync([AsParameters] LogsFilterDto query, ILogsService service)
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

        private static async Task<IResult> GetByIdAsync(int id, ILogsService service)
        {
            var log = await service.GetByIdAsync(id);
            return log is null ? Results.NotFound() : Results.Ok(log);
        }

        private static async Task<IResult> CreateAsync(LogsCreateDto dto, ILogsService service)
        {
            try
            {
                var log = await service.CreateAsync(dto);
                return Results.Ok(log);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> UpdateAsync(LogsUpdateDto dto, ILogsService service)
        {
            try
            {
                var product = await service.UpdateAsync(dto);
                return Results.Ok(product);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        private static async Task<IResult> RemoveAsync(int id, ILogsService service)
        {
            var success = await service.RemoveAsync(id);
            return success ? Results.Ok("Log has been removed successfully.") : Results.NotFound();
        }
    }
}
