using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_SecondaryOptionGroup_RemoveColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SecondaryOptionGroupId",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products",
                column: "PrimaryOptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OptionGroups_PrimaryOptionGroupId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SecondaryOptionGroupId",
                table: "Products",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
