using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Image_ThumbnailImageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products");

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
        }
    }
}
