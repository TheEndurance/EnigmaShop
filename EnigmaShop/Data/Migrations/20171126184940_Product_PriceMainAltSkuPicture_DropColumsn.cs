using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_PriceMainAltSkuPicture_DropColumsn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SKUPictures_AltSKUPictureId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SKUPictures_MainSKUPictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AltSKUId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainSKUId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AltSKUId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainSKUId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AltSKUId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainSKUId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AltSKUId",
                table: "Products",
                column: "AltSKUId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainSKUId",
                table: "Products",
                column: "MainSKUId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SKUPictures_AltSKUId",
                table: "Products",
                column: "AltSKUId",
                principalTable: "SKUPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SKUPictures_MainSKUId",
                table: "Products",
                column: "MainSKUId",
                principalTable: "SKUPictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
