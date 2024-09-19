using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabate.API.Controllers
{
	public class ProductController : APIBaseController
	{
		private readonly IGenaricRepository<Product> _ProductRepo;

		public ProductController(IGenaricRepository<Product> ProductRepo)
		{
			_ProductRepo = ProductRepo;
		}

		#region GetAll
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			//var Products = await _ProductRepo.GetAllAsync();
			var Spec = new ProductWithBrandAndTypeSpecifications();
			var Products = await _ProductRepo.GetAllWithSpecAsync(Spec);
			return Ok(Products);
		}
		#endregion

		#region GetById
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProductById(int id)
		{
			//var Product = await _ProductRepo.GetByIdAsync(id);
			var Spec = new ProductWithBrandAndTypeSpecifications(id);
			var Product = await _ProductRepo.GetByIdWithSpecAsync(Spec);
			return Ok(Product);
		}
		#endregion
	}
}
