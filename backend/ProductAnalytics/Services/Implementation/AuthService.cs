using ProductAnalytics.Utils;
using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Data;
using ProductAnalytics.Models;
using ProductAnalytics.Services.Interfaces;

namespace ProductAnalytics.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ProductAnalyticsDbContext context;

        public AuthService(ProductAnalyticsDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user is null) return null;

            if (!PasswordHasher.Verify(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
