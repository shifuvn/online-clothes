using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v162 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AccountUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AccountUsers",
                type: "text",
                nullable: true);
        }
    }
}
