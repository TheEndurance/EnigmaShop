using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_PriceAndDiscountedPrice_RemoveColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "SKUOptions");

            migrationBuilder.DropColumn(
                name: "IsDiscounted",
                table: "SKUOptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SKUOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "SKUOptions",
                nullable: false,
                defaultValue: 0.00m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscounted",
                table: "SKUOptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SKUOptions",
                nullable: false,
                defaultValue: 0.00m);
        }
    }
}
