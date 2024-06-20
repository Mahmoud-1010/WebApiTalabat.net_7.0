
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Data;
using DataAccessLayer.Entities.Identity;
using DataAccessLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TalabatApi.Extantions;
using TalabatApi.Helpers;

namespace TalabatApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationStoreDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));
            builder.Services.AddDbContext<AppIdentityDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddAplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwagerServices();

            var app = builder.Build();

            //===================
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<ApplicationStoreDbContext>();
                await dbContext.Database.MigrateAsync();// Apply Migrations
                await ApplicationStoreContextSeed.SeedAsync(dbContext,LoggerFactory);

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
                var userManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an errorr occured during apply the migration");
            }
            //============
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWare();
            }
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("Errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
