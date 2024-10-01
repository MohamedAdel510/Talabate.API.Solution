using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabate.API.Errors;
using Talabate.API.Helper;

namespace Talabate.API.Extentions
{
	public static class ApplicationServicesExtintions
	{
		public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
		{
			Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
			Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
			Services.AddAutoMapper(typeof(MappingProfile));
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
														 .SelectMany(M => M.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToArray();
					var validationErrorResponse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(validationErrorResponse);
				};
			});
			return Services;
		}
	}
}
