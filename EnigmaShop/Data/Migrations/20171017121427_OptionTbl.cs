using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class OptionTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_Option_OptionId",
                table: "SKUOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Options",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OptionGroupId",
                table: "Options",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionGroupId",
                table: "Options",
                column: "OptionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionGroup_OptionGroupId",
                table: "Options",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_Options_OptionId",
                table: "SKUOptions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionGroup_OptionGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_Options_OptionId",
                table: "SKUOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionGroupId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "OptionGroupId",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_Option_OptionId",
                table: "SKUOptions",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
