using Serilog;
using Serilog.Events;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Middleware;
using reeltok.api.recommendations.Repositories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace RecommendationsServiceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

            // Add services to the container.
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IVideosService, VideosService>();
            builder.Services.AddScoped<IWatchedVideosService, WatchedVideoService>();
            builder.Services.AddScoped<IRecommendationsService, RecommendationsService>();

            builder.Services.AddScoped<IWatchedVideosRepository, WatchedVideosRepository>();
            builder.Services.AddScoped<IUserInterestsRepository, UserInterestsRepository>();
            builder.Services.AddScoped<IRecommendationsRepository, RecommendationsRepository>();
            builder.Services.AddScoped<IVideoCategoriesRepository, VideoCategoriesRepository>();

            builder.Services.AddDbContext<RecommendationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RecommendationsDb"));
            });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                });

            builder.Services.AddHttpClient();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

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
