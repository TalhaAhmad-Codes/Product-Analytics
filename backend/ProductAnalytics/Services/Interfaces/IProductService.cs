using ProductAnalytics.DTOs.ProductDTOs;

namespace ProductAnalytics.Services.Interfaces
{
    public interface IProductService : IBaseService<ProductResponseDto, ProductCreateDto, ProductFilterDto, ProductUpdateDto>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
