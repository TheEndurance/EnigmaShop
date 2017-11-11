using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_PrimaryOptionGroup_RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OptionGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OptionGroupId",
                table: "Products",
                column: "OptionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_OptionGroupId",
                table: "Products",
                column: "OptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_OptionGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OptionGroupId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryOptionGroupId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PrimaryOptionGroupId",
                table: "Products",
                column: "PrimaryOptionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products",
                column: "PrimaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
