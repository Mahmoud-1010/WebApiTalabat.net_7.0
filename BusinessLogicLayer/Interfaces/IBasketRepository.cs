using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string BasketId);
        Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
