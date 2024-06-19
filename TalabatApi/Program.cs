
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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


            builder.Services.AddAplicationServices();




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
