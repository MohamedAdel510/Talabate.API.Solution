using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabate.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configuration Services

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); 
			builder.Services.AddDbContext<TalabatDbContext>(option => 
			{ 
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			#endregion


			var app = builder.Build();


			#region Update-Database
			using var Scope = app.Services.CreateScope();
			var Services = Scope.ServiceProvider;
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var DbContext = Services.GetRequiredService<TalabatDbContext>();
				await DbContext.Database.MigrateAsync();
				await TalabatDbContextSeed.SeedAsync(DbContext);
			}
			catch (Exception ex)
			{
				var Logger = LoggerFactory.CreateLogger<Program>();
				Logger.LogError(ex, "An error occured during appling migration");
			}
			#endregion

			#region Kestral Pipeline
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();
			#endregion

			app.Run();
		}
	}
}
