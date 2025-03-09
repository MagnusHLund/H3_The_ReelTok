
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

            // Add services to the container.
            builder.Services.AddScoped<IWatchedVideoService, WatchedVideoService>();
            builder.Services.AddScoped<IRecommendationsService, RecommendationsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IVideoRecommendationService, VideosService>();

            builder.Services.AddScoped<IWatchedVideoRepository, WatchedVideoRepository>();
            builder.Services.AddScoped<IRecommendationsRepository, RecommendationRepository>();
            builder.Services.AddScoped<IUserRecommendationRepository, UserRecommendationRepository>();
            builder.Services.AddScoped<IVideoRecommendationRepository, VideoRecommendationRepository>();
            builder.Services.AddScoped<IVideoRecommendationAlgorithmRepository, VideoRecommendationAlgorithmRepository>();

            builder.Services.AddDbContext<RecommendationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("RecommendationsDb"));
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
