using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
	{
		private readonly TalabatDbContext _dbContext;
		public GenaricRepository(TalabatDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if(typeof(T) == typeof(Product))
				return (IEnumerable<T>) await _dbContext.Products.Include(P => P.ProductBrand).Include(P=>P.ProductType).ToListAsync();
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
			=> await _dbContext.Set<T>().FindAsync(id);

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec)
		{
			return await ApplyeSpecification(Spec).ToListAsync();
		}

		public async Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec)
		{
			return await ApplyeSpecification(Spec).FirstOrDefaultAsync();
		}
		private IQueryable<T> ApplyeSpecification(ISpecification<T> Spec)
		{
			return  SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), Spec);
		}
	}
}
