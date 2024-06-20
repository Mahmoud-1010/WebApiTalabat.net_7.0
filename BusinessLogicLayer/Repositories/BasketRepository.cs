using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
            ;
        }
        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var basket = await _database.StringGetAsync(Id);
            return basket.IsNull ? null :
            JsonSerializer.Deserialize<CustomerBasket?>(basket);
        }
        public async Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket Basket)
        {
            var UpdateOrCreateBasket = await _database.StringSetAsync(Basket.Id,
            JsonSerializer.Serialize(Basket), TimeSpan.FromDays(1));
            if (UpdateOrCreateBasket is false) return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
