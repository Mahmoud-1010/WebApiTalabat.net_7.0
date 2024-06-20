using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Specification;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatApi.Dtos;
using TalabatApi.Errors;
using TalabatApi.Helpers;
namespace TalabatApi.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        //private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork,
            //IGenericRepository<Product> productRepo, 
            //IGenericRepository<ProductBrand> productBrandRepo, 
            //IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            //_productRepo = productRepo;
            //_productBrandRepo = productBrandRepo;
            //_productTypeRepo = productTypeRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(specParams);

            var countSpec = new ProductWithFiltersForCountSpecification(specParams);
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(countSpec);
            var products =await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            if (products == null) return NotFound(new ApiResponse(404));
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrands()
        {
            var productBrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (productBrands == null) return NotFound(new ApiResponse(404));
            return Ok(productBrands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllTypes()
        {
            var productTypes = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            if (productTypes == null) return NotFound(new ApiResponse(404));
            return Ok(productTypes);
        }
    }
}
