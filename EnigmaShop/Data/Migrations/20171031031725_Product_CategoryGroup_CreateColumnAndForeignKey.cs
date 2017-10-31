using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_CategoryGroup_CreateColumnAndForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryGroupId",
                table: "Products",
                column: "CategoryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryGroups_CategoryGroupId",
                table: "Products",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryGroups_CategoryGroupId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryGroupId",
                table: "Products");
        }
    }
}
