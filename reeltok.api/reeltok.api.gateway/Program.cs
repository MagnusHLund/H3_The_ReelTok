using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using reeltok.api.gateway.Middleware;
using reeltok.api.gateway.ActionFilters;

namespace GatewayServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<IAuthService, AuthService>();

            builder.Services.AddHttpClient();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
