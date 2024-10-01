using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Talabate.API.Extentions
{
	public static class AddSwaggerExtension
	{
		public static WebApplication UseSwaggerMiddlewares (this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}
	}
}
