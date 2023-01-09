using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineClothes.Persistence.Migrations
{
    public partial class v161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUsers_Image_AvatarImageId",
                table: "AccountUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarImageId",
                table: "AccountUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUsers_Image_AvatarImageId",
                table: "AccountUsers",
                column: "AvatarImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUsers_Image_AvatarImageId",
                table: "AccountUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarImageId",
                table: "AccountUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUsers_Image_AvatarImageId",
                table: "AccountUsers",
                column: "AvatarImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
