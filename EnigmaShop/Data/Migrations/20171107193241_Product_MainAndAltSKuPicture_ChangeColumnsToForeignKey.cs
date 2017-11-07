using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_MainAndAltSKuPicture_ChangeColumnsToForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "AltSKUId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainSKUId",
                table: "Products",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SKUPictures_AltSKUId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SKUPictures_MainSKUId",
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

            migrationBuilder.AddColumn<string>(
                name: "AltImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Products",
                nullable: true);
        }
    }
}
