using Serilog;
using Serilog.Events;
using Newtonsoft.Json;
using reeltok.api.users.Data;
using reeltok.api.users.utils;
using reeltok.api.users.Services;
using reeltok.api.users.factories;
using reeltok.api.users.Middleware;
using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Repositories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Factories;
using reeltok.api.users.Interfaces.Repositories;

namespace UsersServiceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "./Logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ILikesService, LikesService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ILikesRepository, LikesRepository>();
            builder.Services.AddScoped<IEndpointFactory, EndpointFactory>();
            builder.Services.AddScoped<IExternalApiService, ExternalApiService>();
            builder.Services.AddScoped<ISubscriptionsService, SubscriptionsService>();
            builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

            builder.Services.AddDbContextFactory<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb"));
            });

            builder.Services.AddSingleton(sp => new AppSettingsUtils(builder.Configuration));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient<IHttpService, HttpService>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                Log.Information("Starting up");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
