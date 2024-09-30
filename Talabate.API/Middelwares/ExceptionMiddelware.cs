using System.Net;
using System.Text.Json;
using Talabate.API.Errors;

namespace Talabate.API.Middelwares
{
	public class ExceptionMiddelware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddelware> logger;
		private readonly IHostEnvironment env;

		public ExceptionMiddelware(RequestDelegate next, ILogger<ExceptionMiddelware> logger, IHostEnvironment env)
        {
			this.next = next;
			this.logger = logger;
			this.env = env;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

				var exceptionErrorResponse = env.IsDevelopment() ?
					new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
					:
					new ApiExceptionResponse(500);
				var json = JsonSerializer.Serialize(exceptionErrorResponse);
				await context.Response.WriteAsync(json);
			}
		}
    }
}
