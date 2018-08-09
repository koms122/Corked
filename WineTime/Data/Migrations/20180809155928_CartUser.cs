using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class CartUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "WineCarts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WineCartID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WineCarts_ApplicationUserID",
                table: "WineCarts",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WineCarts_AspNetUsers_ApplicationUserID",
                table: "WineCarts",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineCarts_AspNetUsers_ApplicationUserID",
                table: "WineCarts");

            migrationBuilder.DropIndex(
                name: "IX_WineCarts_ApplicationUserID",
                table: "WineCarts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "WineCarts");

            migrationBuilder.DropColumn(
                name: "WineCartID",
                table: "AspNetUsers");
        }
    }
}
