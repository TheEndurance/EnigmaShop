using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class OptionGroup_CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionGroup_OptionGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionGroup",
                table: "OptionGroup");

            migrationBuilder.RenameTable(
                name: "OptionGroup",
                newName: "OptionGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionGroups",
                table: "OptionGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options",
                column: "OptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_OptionGroups_OptionGroupId",
                table: "SKUOptions",
                column: "OptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_OptionGroups_OptionGroupId",
                table: "SKUOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionGroups",
                table: "OptionGroups");

            migrationBuilder.RenameTable(
                name: "OptionGroups",
                newName: "OptionGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionGroup",
                table: "OptionGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionGroup_OptionGroupId",
                table: "Options",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
