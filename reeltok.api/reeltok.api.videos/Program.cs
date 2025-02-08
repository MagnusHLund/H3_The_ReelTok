
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Middleware;
using reeltok.api.videos.Repositories;
using reeltok.api.videos.Services;

namespace VideosServiceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
            builder.Services.AddScoped<ILikesService, LikesService>();
            builder.Services.AddScoped<IHttpService, HttpService>();  // Register HttpService if not already registered
            builder.Services.AddScoped<ILikesRepository, LikesRepository>();

            builder.Services.AddHttpClient();

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
