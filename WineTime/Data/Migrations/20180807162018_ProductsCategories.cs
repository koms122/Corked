using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class ProductsCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WineCategoryName",
                table: "WineProducts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WineProducts_WineCategoryName",
                table: "WineProducts",
                column: "WineCategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_WineProducts_WineCategories_WineCategoryName",
                table: "WineProducts",
                column: "WineCategoryName",
                principalTable: "WineCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineProducts_WineCategories_WineCategoryName",
                table: "WineProducts");

            migrationBuilder.DropIndex(
                name: "IX_WineProducts_WineCategoryName",
                table: "WineProducts");

            migrationBuilder.DropColumn(
                name: "WineCategoryName",
                table: "WineProducts");
        }
    }
}
