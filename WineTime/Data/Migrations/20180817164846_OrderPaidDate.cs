using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WineTime.Data.Migrations
{
    public partial class OrderPaidDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "WineOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "WineOrders");
        }
    }
}
