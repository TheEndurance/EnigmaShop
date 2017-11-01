using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class ProductCategory_Categories_ChangeToSingleCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductCategories_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryGroupId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProductCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Categories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProductCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryGroupId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductCategories_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
