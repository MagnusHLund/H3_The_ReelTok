
using reeltok.api.videos.Data;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Services;
using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Middleware;
using reeltok.api.videos.Repositories;

namespace reeltok.api.videos
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<ILikesService, LikesService>();
            builder.Services.AddScoped<IVideosService, VideosService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<ILikesRepository, LikesRepository>();
            builder.Services.AddScoped<IVideosRepository, VideosRepository>();

            builder.Services.AddDbContext<VideosDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("VideosDb")));

            builder.Services.AddSingleton(sp => new AppSettingsUtils(builder.Configuration));

            builder.Services.AddHttpClient<IHttpService, HttpService>();

            builder.Services.AddControllers();
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

            app.Run();
        }
    }
}
