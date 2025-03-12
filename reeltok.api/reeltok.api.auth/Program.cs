using Serilog;
using Serilog.Events;
using reeltok.api.auth.Data;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Services;
using reeltok.api.auth.Middleware;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Repositories;
using Newtonsoft.Json.Serialization;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Interfaces.Repositories;
using reeltok.api.auth.BackgroundServices;
using Newtonsoft.Json;

namespace AuthServiceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "./Logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ITokenValidationService, TokenValidationService>();
            builder.Services.AddScoped<ITokenManagementService, TokenManagementService>();
            builder.Services.AddScoped<ITokenGenerationService, TokenGenerationService>();
            builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

            builder.Services.AddTransient<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ITokensRepository, TokensRepository>();

            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb"));
            });

            builder.Services.AddSingleton(sp => new AppSettingsUtils(builder.Configuration));

            builder.Services.AddHostedService(provider => new TokenExpirationBackgroundService(provider, TimeSpan.FromHours(8)));

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TokenValidationMiddleware>();

            // Configure the HTTP request pipeline.
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
