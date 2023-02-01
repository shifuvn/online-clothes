using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineClothes.Application.Persistence;
using OnlineClothes.Application.Persistence.Schemas.Products;
using OnlineClothes.BuildIn.Utilities.Extensions;
using OnlineClothes.Domain.Entities;

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

	public override async Task<Product> GetByIntKey(int key, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.AsQueryable()
			.Include(q => q.ProductSkus)
			.Include(q => q.ThumbnailImage)
			.Include(q => q.ProductCategories)
			.ThenInclude(q => q.Category)
			.Include(q => q.Brand)
			.Include(q => q.ProductType)
			.FirstAsync(q => q.Id == key, cancellationToken);

		return entry;
	}

	public async Task EditOneAsync(
		int id,
		PutProductInRepoObject @object,
		CancellationToken cancellationToken = default)
	{
		var product = await AsQueryable()
			.Include(product => product.ProductCategories)
			.FirstAsync(product => product.Id == id, cancellationToken);


		Update(product);
		_mapper.Map(@object, product);

		product.ProductCategories =
			@object.CategoryIds.SelectList(x => new ProductCategory { ProductId = product.Id, CategoryId = x });
	}
}
