using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Persistence.Schemas.Products;
using OnlineClothes.Domain.Entities;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Infrastructure.Repositories;

public class ProductRepository : EfCoreRepositoryBase<Product, int>, IProductRepository
{
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;


	public ProductRepository(AppDbContext dbContext,
		ICategoryRepository categoryRepository,
		IMapper mapper) : base(dbContext)
	{
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<Product> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task EditOneAsync(
		int id,
		PutProductInRepoObject @object,
		CancellationToken cancellationToken = default)
	{
		var product = await AsQueryable()
			.Include(product => product.ProductCategories)
			.FirstAsync(product => product.Id == id, cancellationToken);

		var currentProductCategoryIds = product.ProductCategories.SelectList(pc => pc.CategoryId);
		var newIncomeCategoryIds = @object.CategoryIds.Except(currentProductCategoryIds).ToList();

		Update(product);
		_mapper.Map(@object, product);

		if (newIncomeCategoryIds.Count == 0)
		{
			return; // skip if no category change
		}

		//var navigationCategories = await _categoryRepository
		//	.FindAsync(
		//	FilterBuilder<Category>.Where(q => newIncomeCategoryIds.Contains(q.Id)),
		//	cancellationToken: cancellationToken);

		var navigationCategories =
			newIncomeCategoryIds.SelectList(x => new ProductCategory { ProductId = product.Id, CategoryId = x });

		var productCategoriesList = product.ProductCategories.ToList();
		productCategoriesList.RemoveAll(q => !@object.CategoryIds.Contains(q.CategoryId));
		productCategoriesList.AddRange(navigationCategories);

		// re-assign
		product.ProductCategories = productCategoriesList;
	}
}
