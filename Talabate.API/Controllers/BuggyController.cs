using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;
using Talabate.API.Errors;

namespace Talabate.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BuggyController : ControllerBase
	{
		private readonly TalabatDbContext _dbContext;

		public BuggyController(TalabatDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		[HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = _dbContext.Products.Find(100);
			if (product is null) return NotFound(new ApiResponse(404));
			return Ok(product);
		}
		[HttpGet("ServerError")]
		public ActionResult GetServerError()
		{
			var product = _dbContext.Products.Find(100);
			var ProductToReturn = product.ToString();
			return Ok(ProductToReturn);
		}
		[HttpGet("BadRequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}
		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
	}
}
