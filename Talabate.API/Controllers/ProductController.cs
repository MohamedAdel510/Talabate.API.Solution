using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabate.API.DTOS;
using Talabate.API.Errors;

namespace Talabate.API.Controllers
{
	public class ProductController : APIBaseController
	{
		private readonly IGenaricRepository<Product> _ProductRepo;
		private readonly IMapper _mapper;
		private readonly IGenaricRepository<ProductType> typeRepo;
		private readonly IGenaricRepository<ProductBrand> brandRepo;

		public ProductController(IGenaricRepository<Product> ProductRepo, IMapper mapper, IGenaricRepository<ProductType> typeRepo, IGenaricRepository<ProductBrand> brandRepo)
		{
			_ProductRepo = ProductRepo;
			_mapper = mapper;
			this.typeRepo = typeRepo;
			this.brandRepo = brandRepo;
		}

		#region GetAll
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]ProductSpecParams Params)
		{
			//var Products = await _ProductRepo.GetAllAsync();
			var Spec = new ProductWithBrandAndTypeSpecifications(Params); 
			var Products = await _ProductRepo.GetAllWithSpecAsync(Spec);
			var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
			return Ok(MappedProducts);
		}
		#endregion

		#region GetById
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			//var Product = await _ProductRepo.GetByIdAsync(id);
			var Spec = new ProductWithBrandAndTypeSpecifications(id);
			var Product = await _ProductRepo.GetByIdWithSpecAsync(Spec);
			if(Product is null) return NotFound(new ApiResponse(404));
			var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
			return Ok(MappedProduct);
		}
		#endregion

		#region GetAllTypes
		[HttpGet("Types")]
		public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
		{
			var Types = await this.typeRepo.GetAllAsync();
			return Ok(Types);
		}
		#endregion

		#region GetAllBrands
		[HttpGet("Brands")]
		public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
		{
			var Brands = await this.brandRepo.GetAllAsync();
			return Ok(Brands);
		}
		#endregion
	}
}
