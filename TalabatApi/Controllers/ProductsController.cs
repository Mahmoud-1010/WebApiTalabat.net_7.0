using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Specification;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatApi.Dtos;

namespace TalabatApi.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products =await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
    }
}
