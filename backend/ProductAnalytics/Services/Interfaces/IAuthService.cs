using ProductAnalytics.Models;

namespace ProductAnalytics.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
