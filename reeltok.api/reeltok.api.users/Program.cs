using reeltok.api.users.Repositories;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Services;
using reeltok.api.users.Data;
using Microsoft.EntityFrameworkCore;

namespace UsersServiceApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<ILikeVideosRepository, LikeVideosRepository>();
            builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ILikeVideosService, LikeVideosService>();
            builder.Services.AddScoped<ISubscriptionsService, SubscriptionsService>();

            builder.Services.AddDbContextFactory<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
