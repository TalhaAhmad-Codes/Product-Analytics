
using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Data;
using ProductAnalytics.Endpoints;
using ProductAnalytics.Services.Implementation;
using ProductAnalytics.Services.Interfaces;

namespace ProductAnalytics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database context
            builder.Services.AddDbContext<ProductAnalyticsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ILogsService, LogsService>();

            // Configs
            //builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Endpoints
            app.MapUserEndpoints();
            app.MapProductEndpoints();
            app.MapLogsEndpoints();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.MapControllers();

            app.Run();
        }
    }
}
