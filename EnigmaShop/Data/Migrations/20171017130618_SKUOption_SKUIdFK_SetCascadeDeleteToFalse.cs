using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOption_SKUIdFK_SetCascadeDeleteToFalse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions",
                column: "SKUId",
                principalTable: "SKUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUOptions_SKUs_SKUId",
                table: "SKUOptions",
                column: "SKUId",
                principalTable: "SKUs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
