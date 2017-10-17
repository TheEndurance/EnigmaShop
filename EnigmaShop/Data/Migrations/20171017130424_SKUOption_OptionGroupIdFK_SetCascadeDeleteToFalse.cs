using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_OptionGroupIdFK_SetCascadeDeleteToFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                table: "SKUOptions",
                column: "OptionGroupId",
                principalTable: "OptionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
