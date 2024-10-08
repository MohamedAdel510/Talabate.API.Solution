﻿using AutoMapper;
using Talabat.Core.Entities;
using Talabate.API.DTOS;

namespace Talabate.API.Helper
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureURL))
				return $"{_configuration["ApiBaseUrl"]}{source.PictureURL}";
			return string.Empty;
		}
	}
}
