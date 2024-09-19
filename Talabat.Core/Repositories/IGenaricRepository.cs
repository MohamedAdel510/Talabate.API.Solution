using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
	public interface IGenaricRepository<T> where T : BaseEntity
	{
		#region without specficatoin
		public Task<IEnumerable<T>> GetAllAsync();
		public Task<T> GetByIdAsync(int id);
		#endregion

		#region With Specficatoin
		public Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
		public Task<T> GetByIdWithSpecAsync(ISpecification<T> Spec);
		#endregion
	}
}
