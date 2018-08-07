using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class Carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineProducts_WineCategories_WineCategoryName",
                table: "WineProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineProducts",
                table: "WineProducts");

            migrationBuilder.RenameTable(
                name: "WineProducts",
                newName: "WineProduct");

            migrationBuilder.RenameIndex(
                name: "IX_WineProducts_WineCategoryName",
                table: "WineProduct",
                newName: "IX_WineProduct_WineCategoryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineProduct",
                table: "WineProduct",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "WineCarts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineCarts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WineCartProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WineCartID = table.Column<int>(nullable: false),
                    WineProductsID = table.Column<int>(nullable: true),
                    WineProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineCartProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WineCartProducts_WineCarts_WineCartID",
                        column: x => x.WineCartID,
                        principalTable: "WineCarts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WineCartProducts_WineProduct_WineProductsID",
                        column: x => x.WineProductsID,
                        principalTable: "WineProduct",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WineCartProducts_WineCartID",
                table: "WineCartProducts",
                column: "WineCartID");

            migrationBuilder.CreateIndex(
                name: "IX_WineCartProducts_WineProductsID",
                table: "WineCartProducts",
                column: "WineProductsID");

            migrationBuilder.AddForeignKey(
                name: "FK_WineProduct_WineCategories_WineCategoryName",
                table: "WineProduct",
                column: "WineCategoryName",
                principalTable: "WineCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineProduct_WineCategories_WineCategoryName",
                table: "WineProduct");

            migrationBuilder.DropTable(
                name: "WineCartProducts");

            migrationBuilder.DropTable(
                name: "WineCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WineProduct",
                table: "WineProduct");

            migrationBuilder.RenameTable(
                name: "WineProduct",
                newName: "WineProducts");

            migrationBuilder.RenameIndex(
                name: "IX_WineProduct_WineCategoryName",
                table: "WineProducts",
                newName: "IX_WineProducts_WineCategoryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WineProducts",
                table: "WineProducts",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_WineProducts_WineCategories_WineCategoryName",
                table: "WineProducts",
                column: "WineCategoryName",
                principalTable: "WineCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
