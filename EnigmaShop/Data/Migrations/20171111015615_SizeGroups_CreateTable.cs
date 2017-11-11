using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SizeGroups_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SizeGroup_SizeGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_SizeGroup_SizeGroupId",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SizeGroup",
                table: "SizeGroup");

            migrationBuilder.RenameTable(
                name: "SizeGroup",
                newName: "SizeGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SizeGroups",
                table: "SizeGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SizeGroups_SizeGroupId",
                table: "Products",
                column: "SizeGroupId",
                principalTable: "SizeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_SizeGroups_SizeGroupId",
                table: "Sizes",
                column: "SizeGroupId",
                principalTable: "SizeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SizeGroups_SizeGroupId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_SizeGroups_SizeGroupId",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SizeGroups",
                table: "SizeGroups");

            migrationBuilder.RenameTable(
                name: "SizeGroups",
                newName: "SizeGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SizeGroup",
                table: "SizeGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SizeGroup_SizeGroupId",
                table: "Products",
                column: "SizeGroupId",
                principalTable: "SizeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_SizeGroup_SizeGroupId",
                table: "Sizes",
                column: "SizeGroupId",
                principalTable: "SizeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
