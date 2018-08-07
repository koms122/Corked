using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class WineID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineCartProducts_WineProduct_WineProductsID",
                table: "WineCartProducts");

            migrationBuilder.DropColumn(
                name: "WineProductID",
                table: "WineCartProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "WineProduct",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateLastModified",
                table: "WineProduct",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "WineProduct",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "WineProductsID",
                table: "WineCartProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WineCartProducts_WineProduct_WineProductsID",
                table: "WineCartProducts",
                column: "WineProductsID",
                principalTable: "WineProduct",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WineCartProducts_WineProduct_WineProductsID",
                table: "WineCartProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Schedule",
                table: "WineProduct",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateLastModified",
                table: "WineProduct",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "WineProduct",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WineProductsID",
                table: "WineCartProducts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "WineProductID",
                table: "WineCartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WineCartProducts_WineProduct_WineProductsID",
                table: "WineCartProducts",
                column: "WineProductsID",
                principalTable: "WineProduct",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
