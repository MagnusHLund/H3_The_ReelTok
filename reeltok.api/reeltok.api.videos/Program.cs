using Serilog;
using Serilog.Events;
using Newtonsoft.Json;
using reeltok.api.videos.Data;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Services;
using reeltok.api.videos.Factories;
using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Middleware;
using reeltok.api.videos.Repositories;
using reeltok.api.videos.Interfaces.Services;
using reeltok.api.videos.Interfaces.Factories;

namespace reeltok.api.videos
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
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ILikesService, LikesService>();
            builder.Services.AddScoped<IVideosService, VideosService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<ILikesRepository, LikesRepository>();
            builder.Services.AddScoped<IEndpointFactory, EndpointFactory>();
            builder.Services.AddScoped<IVideosRepository, VideosRepository>();
            builder.Services.AddScoped<IExternalApiService, ExternalApiService>();
            builder.Services.AddScoped<IThumbnailService, ThumbnailService>();

            builder.Services.AddDbContext<VideosDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("VideosDb")));

            builder.Services.AddSingleton(sp => new AppSettingsUtils(builder.Configuration));

            builder.Services.AddHttpClient<IHttpService, HttpService>();

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
