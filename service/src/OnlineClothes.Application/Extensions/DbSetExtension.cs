using Microsoft.EntityFrameworkCore;
using OnlineClothes.Support.Utilities.Extensions;

namespace OnlineClothes.Application.Extensions;

public static class DbSetExtension
{
	public static void TryTrackManyToMany<T, TKey>(this DbSet<T> dbSet, ICollection<T> currents, ICollection<T> news,
		Func<T, TKey> getKeySelector) where T : class
	{
		dbSet.RemoveRange(currents.Except(news, getKeySelector));
		dbSet.AddRange(news.Except(currents, getKeySelector));
	}
}
