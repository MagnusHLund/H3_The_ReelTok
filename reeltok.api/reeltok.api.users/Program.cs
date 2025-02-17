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
            builder.Services.AddScoped<ILikeVideoRepository, LikeVideoRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ILikeVideoService, LikeVideoService>();
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

            builder.Services.AddDbContextFactory<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
