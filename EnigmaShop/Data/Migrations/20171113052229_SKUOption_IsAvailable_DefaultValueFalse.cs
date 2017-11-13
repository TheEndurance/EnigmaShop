using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_IsAvailable_DefaultValueFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "IsDiscounted",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SKUs");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "SKUSizes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "SKUSizes",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "SKUs",
                nullable: false,
                defaultValue: 0.00m);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "SKUs",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscounted",
                table: "SKUs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SKUs",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
