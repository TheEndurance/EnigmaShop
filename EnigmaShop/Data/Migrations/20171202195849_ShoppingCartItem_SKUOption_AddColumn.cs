using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class ShoppingCartItem_SKUOption_AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SKUOptionId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_SKUOptionId",
                table: "ShoppingCartItems",
                column: "SKUOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_SKUOptions_SKUOptionId",
                table: "ShoppingCartItems",
                column: "SKUOptionId",
                principalTable: "SKUOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_SKUOptions_SKUOptionId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_SKUOptionId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "SKUOptionId",
                table: "ShoppingCartItems");
        }
    }
}
