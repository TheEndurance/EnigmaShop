using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_Stock_CreateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "SKUOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryOptionGroupId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products",
                column: "SecondaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "SKUOptions");

            migrationBuilder.AlterColumn<int>(
                name: "SecondaryOptionGroupId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products",
                column: "SecondaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
