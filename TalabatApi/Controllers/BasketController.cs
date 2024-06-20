using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatApi.Dtos;

namespace TalabatApi.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedBasket = await _basketRepo.UpdateOrCreateBasketAsync(customerBasket);
            //var customerBasket = await _basketRepo.UpdateOrCreateBasketAsync(basket);
            return Ok(customerBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepo.DeleteBasketAsync(basketId);
        }
    }
}
