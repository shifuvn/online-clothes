using OnlineClothes.Domain.Paging;

namespace OnlineClothes.UnitTest.Domain;

[Collection("Domain Paging")]
public class PagingTest
{
	[Theory]
	[Trait("Category", "Domain_Paging")]
	[InlineData(1, 20)]
	[InlineData(2, 30)]
	[InlineData(100, 1)]
	public void PagingRequest_WithUintValue(int pageIndex, int pageSize)
	{
		// arrange
		var pagingReq = new PagingRequest
		{
			PageIndex = pageIndex,
			PageSize = pageSize
		};

		// act

		// assert
		Assert.NotNull(pagingReq);
		Assert.Equal(pageSize, pagingReq.PageSize);
		Assert.Equal(pageIndex, pagingReq.PageIndex);
	}

	[Theory]
	[Trait("Category", "Domain_Paging")]
	[InlineData(-2, -30)]
	[InlineData(0, 0)]
	public void PagingRequest_WithNegativeValue(int pageIndex, int pageSize)
	{
		// arrange
		var pagingReq = new PagingRequest
		{
			PageIndex = pageIndex,
			PageSize = pageSize
		};

		// act

		// assert
		Assert.NotNull(pagingReq);
		Assert.Equal(1, pagingReq.PageIndex);
		Assert.Equal(1, pagingReq.PageSize);
	}
}
