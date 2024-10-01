using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public interface ISpecification<T> where T : BaseEntity
	{
		//_dbContext.Products.Where(P=>P.id == id).Include(P => P.ProductBrand).Include(P=>P.ProductType)

		//1- Where(P=>P.id == id) => Where Conditoin
		public Expression<Func<T, bool>> Criteria { get; set; }

		//2- Include(P => P.ProductBrand).Include(P=>P.ProductType) => Includes
		public List<Expression<Func<T, object>>> Includes { get; set; }
		
		public Expression<Func<T, object>> OrderBy { get; set; }
		public Expression<Func<T, object>> OrderByDec {  get; set; }

		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnable { get; set; }
	}
}
