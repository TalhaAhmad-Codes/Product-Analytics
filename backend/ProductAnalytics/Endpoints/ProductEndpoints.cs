using ProductAnalytics.DTOs.ProductDTOs;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/products");

            // Endpoints
            group.MapGet("/", GetAllAsync);
            group.MapGet("/{id:int}", GetByIdAsync);

            group.MapPost("/", CreateAsync);

            group.MapPut("/", UpdateAsync);

            group.MapDelete("/{id:int}", RemoveAsync);
        }

        /*/ <----- Handlers -----> /*/
        private static async Task<IResult> GetAllAsync([AsParameters] ProductFilterDto query, IProductService service)
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
        
        private static async Task<IResult> GetByIdAsync(int id, IProductService service)
        {
            var product = await service.GetByIdAsync(id);
            return product is null ? Results.NotFound() : Results.Ok(product);
        }

        private static async Task<IResult> CreateAsync(ProductCreateDto dto, IProductService service)
        {
            try
            {
                var product = await service.CreateAsync(dto);
                return Results.Ok(product);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        
        private static async Task<IResult> UpdateAsync(ProductUpdateDto dto, IProductService service)
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
        
        private static async Task<IResult> RemoveAsync(int id, IProductService service)
        {
            var success = await service.RemoveAsync(id);
            return success ? Results.Ok("Product has been removed successfully, including logs.") : Results.
                NotFound();
        }
    }
}
