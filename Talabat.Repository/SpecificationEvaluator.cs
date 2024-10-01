using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvaluator<T> where T : BaseEntity 
	{
		//_dbContext.Products.Where(P=>P.id == id).Include(P => P.ProductBrand).Include(P=>P.ProductType)
		public static IQueryable<T> GetQuery(IQueryable<T> InpputQuery, ISpecification<T> Spec)
		{
			var Query = InpputQuery;  //_dbContext.Products
			if (Spec.Criteria != null)
			{
				Query = Query.Where(Spec.Criteria);  // Where(P=>P.id == id)
			}
			if(Spec.OrderBy is not null)
			{
				Query = Query.OrderBy(Spec.OrderBy);
			}
			if(Spec.OrderByDec is not null)
			{
				Query = Query.OrderByDescending(Spec.OrderByDec);
			}
			if(Spec.IsPaginationEnable)
			{
				Query = Query.Skip(Spec.Skip).Take(Spec.Take);
			}
			// {Expression<Func<T, object>>, Expression<Func<T, object>>}
			Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

			return Query;
		}
	}
}
