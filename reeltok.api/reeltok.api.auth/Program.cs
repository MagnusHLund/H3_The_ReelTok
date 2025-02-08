using reeltok.api.auth.Services;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Repositories;
using reeltok.api.auth.Data;
using reeltok.api.auth.Middleware;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddTransient<IAuthService, AuthService>();
      builder.Services.AddTransient<IAuthRepository, AuthRepository>();
      builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
      builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb")));
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();
      app.UseMiddleware<ExceptionMiddleware>();

			app.MapControllers();

			app.Run();
		}
	}
}
