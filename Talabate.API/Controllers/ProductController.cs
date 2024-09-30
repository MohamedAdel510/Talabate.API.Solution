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

		public ProductController(IGenaricRepository<Product> ProductRepo, IMapper mapper)
		{
			_ProductRepo = ProductRepo;
			_mapper = mapper;
		}

		#region GetAll
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			//var Products = await _ProductRepo.GetAllAsync();
			var Spec = new ProductWithBrandAndTypeSpecifications();
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
	}
}
