using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Data;
using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.DTOs.ProductDTOs;
using ProductAnalytics.Mappers;
using ProductAnalytics.Models;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Services.Implementation
{
    public sealed class ProductService : IProductService
    {
        private readonly ProductAnalyticsDbContext context;

        public ProductService(ProductAnalyticsDbContext context)
        {
            this.context = context;
        }

        public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
        {
            // Product already exists!
            bool exists = await ExistsByNameAsync(dto.Name);

            if (exists)
                throw new DomainException("The product of this name already exists.");

            // Product Creation
            Product product = new()
            {
                Image = dto.Image,
                Name = dto.Name,
                Price = dto.Price,
                StockStatus = dto.StockStatus
            };

            // Adding to database
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return ProductMapper.ToDto(product);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var count = await context.Products.AsQueryable()
                .Where(p => p.Name == name)
                .CountAsync();

            return count > 0;
        }

        public async Task<PagedResultDto<ProductResponseDto>> GetAllAsync(ProductFilterDto filterDto)
        {
            var query = context.Products.AsQueryable();

            // Applying filters
            if (filterDto.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filterDto.MinPrice.Value);

            if (filterDto.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filterDto.MaxPrice.Value);

            if (filterDto.StockStatus.HasValue)
                query = query.Where(p => p.StockStatus == filterDto.StockStatus);

            // Get paged result
            var totalCount = await query.CountAsync();
            var items = await Misc.GetPagedResultAsync<Product>(query, filterDto.PageNumber, filterDto.PageSize);

            return new PagedResultDto<ProductResponseDto>
            {
                Items = [.. items.Select(ProductMapper.ToDto)],
                TotalCount = totalCount
            };
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            return product is null ? null : ProductMapper.ToDto(product);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product is null)
                return false;

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductResponseDto> UpdateAsync(ProductUpdateDto dto)
        {
            var product = await context.Products.FindAsync(dto.Id)
                ?? throw new DomainException("Product not found!");

            // Against duplicate name
            if (product.Name != dto.Name)
            {
                bool exists = await ExistsByNameAsync(dto.Name);

                if (exists)
                    throw new DomainException($"The porduct of name '{dto.Name}' already exists.");
            }

            // Update the product
            product.Image = dto.Image;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.StockStatus = dto.StockStatus;

            context.Products.Update(product);
            await context.SaveChangesAsync();
            return ProductMapper.ToDto(product);
        }
    }
}
