using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_Price_SetDefaultValueToZero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SKUSizes",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0.00m,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedPrice",
                table: "SKUSizes",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0.00m,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SKUSizes",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldDefaultValue: 0.00m);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountedPrice",
                table: "SKUSizes",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)",
                oldDefaultValue: 0.00m);
        }
    }
}
