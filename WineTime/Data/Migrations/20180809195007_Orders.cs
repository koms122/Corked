using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "WineCartProducts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "WineOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    AptSuite = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineOrders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WineOrderProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WineOrderID = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    ProductID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineOrderProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WineOrderProducts_WineOrders_WineOrderID",
                        column: x => x.WineOrderID,
                        principalTable: "WineOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WineOrderProducts_WineOrderID",
                table: "WineOrderProducts",
                column: "WineOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WineOrderProducts");

            migrationBuilder.DropTable(
                name: "WineOrders");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "WineCartProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
