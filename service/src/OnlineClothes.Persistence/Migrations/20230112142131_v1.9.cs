using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
