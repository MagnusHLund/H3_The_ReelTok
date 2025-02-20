
using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;

namespace RecommendationsServiceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContextFactory<RecommendationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

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
