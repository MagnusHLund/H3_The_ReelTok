using Serilog;
using Serilog.Events;
using Newtonsoft.Json;
using reeltok.api.comments.Data;
using reeltok.api.comments.Utils;
using Microsoft.EntityFrameworkCore;
using reeltok.api.comments.Services;
using reeltok.api.comments.Factories;
using reeltok.api.comments.Middleware;
using reeltok.api.comments.Repositories;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.Interfaces.Factories;
using reeltok.api.comments.Interfaces.Repositories;

namespace reeltok.api.comments
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

            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ICommentsService, CommentService>();
            builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

            builder.Services.AddScoped<IEndpointFactory, EndpointFactory>();

            builder.Services.AddScoped<ICommentsRepository, CommentRepository>();

            builder.Services.AddDbContext<CommentsDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CommentsDb"));
            });

            builder.Services.AddSingleton(sp => new AppSettingsUtils(builder.Configuration));

            builder.Services.AddHttpClient<IHttpService, HttpService>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                });

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
