
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Interfaces.repositories;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Middleware;
using reeltok.api.recommendations.Repositories;
using reeltok.api.recommendations.Services;

namespace RecommendationsServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IWatchedVideoService, WatchedVideoService>();
            builder.Services.AddScoped<IRecommendationsService, RecommendationsService>();
            builder.Services.AddScoped<IUserRecommendationService, UserRecommendationService>();
            builder.Services.AddScoped<IVideoRecommendationService, VideoRecommendationService>();

            builder.Services.AddScoped<IWatchedVideoRepository, WatchedVideoRepository>();
            builder.Services.AddScoped<IRecommendationsRepository, RecommendationRepository>();
            builder.Services.AddScoped<IUserRecommendationRepository, UserRecommendationRepository>();
            builder.Services.AddScoped<IVideoRecommendationRepository, VideoRecommendationRepository>();

            builder.Services.AddDbContextFactory<RecommendationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
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

            app.Run();
        }
    }
}
