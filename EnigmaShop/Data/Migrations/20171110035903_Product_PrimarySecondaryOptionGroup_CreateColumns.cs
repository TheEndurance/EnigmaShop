using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_PrimarySecondaryOptionGroup_CreateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryOptionGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondaryOptionGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PrimaryOptionGroupId",
                table: "Products",
                column: "PrimaryOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SecondaryOptionGroupId",
                table: "Products",
                column: "SecondaryOptionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products",
                column: "PrimaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products",
                column: "SecondaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SecondaryOptionGroupId",
                table: "Products");
        }
    }
}
