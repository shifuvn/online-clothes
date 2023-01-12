using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v163 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ProductSkus",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailImageId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId",
                principalTable: "Image",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus");

            migrationBuilder.DropIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProductSkus");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageId",
                table: "Products");
        }
    }
}
