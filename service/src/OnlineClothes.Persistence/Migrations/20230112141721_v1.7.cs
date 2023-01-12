using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus");

            migrationBuilder.DropIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus");

            migrationBuilder.DropIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSkus_ImageId",
                table: "ProductSkus",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSkus_Image_ImageId",
                table: "ProductSkus",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }
    }
}
