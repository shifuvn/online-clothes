using AutoMapper;
using OnlineClothes.Application.Features.Brands.Commands.Create;
using OnlineClothes.Application.Features.Brands.Commands.Edit;
using OnlineClothes.Application.Features.Categories.Commands.Edit;
using OnlineClothes.Application.Features.Products.Commands.CreateNewProductSeri;
using OnlineClothes.Application.Features.Products.Commands.CreateNewSku;
using OnlineClothes.Application.Features.Products.Commands.EditProductInfo;
using OnlineClothes.Application.Features.Products.Commands.EditSkuInfo;
using OnlineClothes.Application.Mapping.ViewModels;
using OnlineClothes.Application.Persistence.Schemas.Products;
using OnlineClothes.Domain.Entities;

namespace OnlineClothes.Application.Mapping;

public class DefaultAutoMapperProfile : Profile
{
	public DefaultAutoMapperProfile()
	{
		// request to model
		CreateMap<CreateBrandCommand, Brand>()
			.ForMember(q => q.Id, opt => opt.Ignore());
		CreateMap<EditBrandCommand, Brand>();
		CreateMap<EditCategoryCommand, Category>();

		CreateMap<CreateSkuCommand, Product>();
		CreateMap<CreateSkuCommand, PutProductInRepoObject>();
		CreateMap<PutProductInRepoObject, Product>()
			.ForMember(dest => dest.BrandId, opt => opt.Condition(q => q.BrandId is not null && q.BrandId != 0))
			.ForMember(dest => dest.ProductCategories, opt => opt.Ignore());

		// Viewmodel
		CreateMap<BrandViewModel, Brand>().ReverseMap();
		CreateMap<CategoryViewModel, Category>().ReverseMap();

		// Verified used

		// Dto
		CreateMap<Product, ProductBasicDto>();
		CreateMap<CreateSkuCommand, ProductSku>();

		CreateMap<CreateNewProductCommand, Product>()
			.ForMember(dest => dest.ProductCategories,
				opt => opt.MapFrom(src =>
					src.CategoryIds.Select(x => new ProductCategory { CategoryId = x, ProductId = 0 })))
			.ForMember(dest => dest.ProductSkus, opt => opt.MapFrom(src => new List<ProductSku>
			{
				new()
				{
					Sku = src.Sku,
					AddOnPrice = src.SkuAddOnPrice,
					InStock = src.SkuInStock,
					Size = src.SkuSize
				}
			}));
		CreateMap<EditSkuInfoCommand, ProductSku>();

		CreateMap<EditProductCommand, PutProductInRepoObject>();
	}
}
