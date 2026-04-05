using Microsoft.EntityFrameworkCore;

namespace ProductAnalytics.Utils
{
    public static class Misc
    {
        public static async Task<List<T>> GetPagedResultAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public static string Simplify(string str, bool toLower = true)
            => toLower ? str.Trim().ToLower() : str.Trim();
    }
}
