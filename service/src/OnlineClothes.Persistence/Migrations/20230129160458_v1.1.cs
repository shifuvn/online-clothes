using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductSkus_TempId1",
                table: "ProductSkus");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "ProductSkus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TempId1",
                table: "ProductSkus",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductSkus_TempId1",
                table: "ProductSkus",
                column: "TempId1");
        }
    }
}
