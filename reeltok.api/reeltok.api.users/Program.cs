using reeltok.api.users.Repositories;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Services;
using reeltok.api.users.Data;
using Microsoft.EntityFrameworkCore;

namespace UsersServiceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Repositories DI
			// builder.Services.AddTransient<IUsersRepository, UsersRepository>();
			// builder.Services.AddTransient<ILikeVideoRepository, LikeVideoRepository>();
			// builder.Services.AddTransient<ISubscriptionRepository, SubscriptionRepository>();
			
			builder.Services.AddScoped<IUsersRepository, UsersRepository>();
			builder.Services.AddScoped<ILikeVideoRepository, LikeVideoRepository>();
			builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

			// Services DI
			// builder.Services.AddTransient<IUsersService, UsersService>();
			// builder.Services.AddTransient<ILikeVideoService, LikeVideoService>();
			// builder.Services.AddTransient<ISubscriptionService, SubscriptionService>();

			builder.Services.AddScoped<IUsersService, UsersService>();
			builder.Services.AddScoped<ILikeVideoService, LikeVideoService>();
			builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

			builder.Services.AddDbContext<UserDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


			app.MapControllers();

			app.Run();
		}
	}
}
