using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
	{
		public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) : 
			base(P => 
			(!Params.BrandId.HasValue||P.ProductBrandId == Params.BrandId)
			&&
			(!Params.TypeId.HasValue||P.ProductTypeId == Params.TypeId)
			) 
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);

			ApplyPagination(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);

			if (!string.IsNullOrEmpty(Params.Sort))
			{
				switch (Params.Sort)
				{
					case "PriceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "PriceDesc":
						AddOrderByDec(P => P.Price);
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}

				// Product = 100 
				// PageSize = 10
				// PageIndex = 5
				// Skip = 40
				// Take = 41 : 50 (10)
				
			}
		}
		public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
	}
}
