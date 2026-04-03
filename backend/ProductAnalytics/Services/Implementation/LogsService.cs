using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProductAnalytics.Data;
using ProductAnalytics.DTOs.CommonDTOs;
using ProductAnalytics.DTOs.LogsDTOs;
using ProductAnalytics.Mappers;
using ProductAnalytics.Models;
using ProductAnalytics.Services.Interfaces;
using ProductAnalytics.Utils;

namespace ProductAnalytics.Services.Implementation
{
    public sealed class LogsService : ILogsService
    {
        private readonly ProductAnalyticsDbContext context;

        public LogsService(ProductAnalyticsDbContext context)
        {
            this.context = context;
        }

        public async Task<LogsResponseDto> CreateAsync(LogsCreateDto dto)
        {
            // Check for duplicates
            bool exists = await ExistsByProductAndSellDateAsync(dto.ProductId, dto.SellDate);

            if (exists)
                throw new DomainException($"This product has already contain logs at date '{dto.SellDate}'.");

            // Logs creation
            Logs log = new()
            {
                ProductId = dto.ProductId,
                SellDate = dto.SellDate,
                Quantity = dto.Quantity
            };

            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
            return LogsMapper.ToDto(log);
        }

        public async Task<bool> ExistsByProductAndSellDateAsync(int productId, DateOnly sellDate)
        {
            var count = await context.Logs.AsQueryable()
                .Where(l => l.ProductId == productId && l.SellDate == sellDate)
                .CountAsync();

            return count > 0;
        }

        public async Task<PagedResultDto<LogsResponseDto>> GetAllAsync(LogsFilterDto filterDto)
        {
            var query = context.Logs.AsQueryable();

            // Applying filters
            if (filterDto.ProductId.HasValue)
                query = query.Where(l => l.ProductId == filterDto.ProductId);

            if (filterDto.FromSellDate.HasValue)
                query = query.Where(l => l.SellDate >= filterDto.FromSellDate);

            if (filterDto.ToSellDate.HasValue)
                query = query.Where(l => l.SellDate <= filterDto.ToSellDate);

            if (filterDto.MinQuantity.HasValue)
                query = query.Where(l => l.Quantity >= filterDto.MinQuantity);

            if (filterDto.MaxQuantity.HasValue)
                query = query.Where(l => l.Quantity <= filterDto.MaxQuantity);

            // Get paged result
            var totalCount = await query.CountAsync();
            var items = await Misc.GetPagedResultAsync<Logs>(query, filterDto.PageNumber, filterDto.PageSize);

            return new PagedResultDto<LogsResponseDto>
            {
                Items = items.Select(LogsMapper.ToDto).ToList(),
                TotalCount = totalCount
            };
        }

        public async Task<LogsResponseDto?> GetByIdAsync(int id)
        {
            var log = await context.Logs.FindAsync(id);
            return log is null ? null : LogsMapper.ToDto(log);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var log = await context.Logs.FindAsync(id);

            if (log is null)
                return false;

            // Remove the log
            context.Logs.Remove(log);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<LogsResponseDto> UpdateAsync(LogsUpdateDto dto)
        {
            var log = await context.Logs.FindAsync(dto.Id)
                ?? throw new DomainException("The log not found!");

            // Update the log
            log.ProductId = dto.ProductId;
            log.SellDate = dto.SellDate;
            log.Quantity = dto.Quantity;

            // Save changes
            context.Logs.Update(log);
            await context.SaveChangesAsync();
            return LogsMapper.ToDto(log);
        }
    }
}
