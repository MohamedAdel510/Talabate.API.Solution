using AutoMapper;
using Talabat.Core.Entities;
using Talabate.API.DTOS;

namespace Talabate.API.Helper
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.PictureURL, o => o.MapFrom<ProductPictureUrlResolver>());
		}
	}
}
