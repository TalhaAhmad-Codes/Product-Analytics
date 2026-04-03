using ProductAnalytics.DTOs.CommonDTOs;

namespace ProductAnalytics.Services.Interfaces
{
    public interface IBaseService<TR, TC, TF> where TR : class
    {
        Task<PagedResultDto<TR>> GetAllAsync(TF filterDto);
        Task<TR?> GetByIdAsync(int id);
        Task<TR> CreateAsync(TC dto);
        Task<bool> RemoveAsync(int id);
    }

    public interface IBaseService<TR, TC, TF, TU> : IBaseService<TR, TC, TF> where TR : class
    {
        Task<TR> UpdateAsync(TU dto);
    }
}
