using OnlineClothes.Domain.Entities;
using OnlineClothes.Persistence.Repositories.Abstracts;

namespace OnlineClothes.Infrastructure.Repositories.Abstracts;

public interface IProductRepository : IRootRepository<ProductClothe, string>
{
}
