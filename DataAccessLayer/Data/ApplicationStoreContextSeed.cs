using DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationStoreContextSeed
    {
        public static async Task SeedAsync(ApplicationStoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../DataAccessLayer/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                        context.ProductBrands.Add(brand);

                    await context.SaveChangesAsync();

                }
                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText("../DataAccessLayer/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types)
                        context.ProductTypes.Add(item);

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../DataAccessLayer/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                        context.Products.Add(item);

                    await context.SaveChangesAsync();
                }
                //if (!context.DeliveryMethods.Any())
                //{
                //    var deliverMethodsData =
                //        File.ReadAllText("../Talabat.DAL/Data/SeedData/delivery.json");
                //    var deliverMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverMethodsData);
                //    foreach (var deliverMethod in deliverMethods)
                //        context.DeliveryMethods.Add(deliverMethod);

                //    await context.SaveChangesAsync();
                //}
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<ApplicationStoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
