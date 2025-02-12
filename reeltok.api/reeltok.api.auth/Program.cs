using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Data;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Middleware;
using reeltok.api.auth.Repositories;
using reeltok.api.auth.Services;
using reeltok.api.auth.Utils;

namespace AuthServiceApi
{
	public static class Program
	{
		public static void Main(string[] args)
		{
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IAuthRepository, AuthRepository>();

            builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDb")));

            builder.Services.AddSingleton(new AppSettingsUtils(builder.Configuration));

			builder.Services.AddControllers(
                options => {
                    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                    options.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
                    options.RespectBrowserAcceptHeader = true;
                    options.ReturnHttpNotAcceptable = true;
                })
                .AddXmlSerializerFormatters()
                .AddXmlDataContractSerializerFormatters();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TokenValidationMiddleware>();

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
