﻿namespace OnlineClothes.Application.Mapping.Dto;

public class ProductSkuDto : ProductSkuBasicDto
{
	public ProductSkuDto(ProductSku domain) : base(domain)
	{
		Description = domain.Product.Description;
		Size = domain.Size.ToString();
		Brand = domain.Product.Brand is null ? null : new BrandDto(domain.Product.Brand!);
		Categories = domain.Product.ProductCategories.SelectList(cate => new CategoryDto(cate.Category));
		ProductId = domain.Product.Id;
	}

	public string? Description { get; set; }
	public string? Size { get; set; }
	public BrandDto? Brand { get; set; }
	public List<CategoryDto> Categories { get; set; } = new();
	public int ProductId { get; set; }
}
