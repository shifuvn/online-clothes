using OnlineClothes.Application.Services.ObjectStorage.Models;

namespace OnlineClothes.UnitTest.ObjectFile;

[Collection("ObjectObject Test")]
public class ObjectStorageTest
{
	[Fact]
	[Trait("Category", "ObjectObject_Key")]
	public void GetAwsIdentifierKey_FromUrl_ShouldValid()
	{
		// arrange
		var url = "https://uitk14-oop-online-clothes.s3.ap-southeast-1.amazonaws.com/product/TP01-2";

		// act
		var key = ObjectStorage.GetIdentifierKey(url);

		// assert
		Assert.Equal("product/TP01-2", key);
	}
}
