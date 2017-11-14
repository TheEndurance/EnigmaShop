using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUSize_RenameTableTo_SKUOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUSizes_SKUs_SKUId",
                table: "SKUSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUSizes_Sizes_SizeId",
                table: "SKUSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SKUSizes",
                table: "SKUSizes");

            migrationBuilder.RenameTable(
                name: "SKUSizes",
                newName: "SKUOptions");

            migrationBuilder.RenameIndex(
                name: "IX_SKUSizes_SizeId",
                table: "SKUOptions",
                newName: "IX_SKUOptions_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_SKUSizes_SKUId",
                table: "SKUOptions",
                newName: "IX_SKUOptions_SKUId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SKUOptions",
                table: "SKUOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions",
                column: "SKUId",
                principalTable: "SKUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_Sizes_SizeId",
                table: "SKUOptions",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_Sizes_SizeId",
                table: "SKUOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SKUOptions",
                table: "SKUOptions");

            migrationBuilder.RenameTable(
                name: "SKUOptions",
                newName: "SKUSizes");

            migrationBuilder.RenameIndex(
                name: "IX_SKUOptions_SizeId",
                table: "SKUSizes",
                newName: "IX_SKUSizes_SizeId");

            migrationBuilder.RenameIndex(
                name: "IX_SKUOptions_SKUId",
                table: "SKUSizes",
                newName: "IX_SKUSizes_SKUId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SKUSizes",
                table: "SKUSizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUSizes_SKUs_SKUId",
                table: "SKUSizes",
                column: "SKUId",
                principalTable: "SKUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUSizes_Sizes_SizeId",
                table: "SKUSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
