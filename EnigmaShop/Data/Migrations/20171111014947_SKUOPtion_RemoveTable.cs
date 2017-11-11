using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOPtion_RemoveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SKUOptions");

            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "SKUs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SKUs_OptionId",
                table: "SKUs",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SKUs_Options_OptionId",
                table: "SKUs",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SKUs_Options_OptionId",
                table: "SKUs");

            migrationBuilder.DropIndex(
                name: "IX_SKUs_OptionId",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "SKUs");

            migrationBuilder.CreateTable(
                name: "SKUOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionGroupId = table.Column<int>(nullable: false),
                    OptionId = table.Column<int>(nullable: false),
                    SKUId = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SKUOptions_OptionGroups_OptionGroupId",
                        column: x => x.OptionGroupId,
                        principalTable: "OptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SKUOptions_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SKUOptions_SKUs_SKUId",
                        column: x => x.SKUId,
                        principalTable: "SKUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SKUOptions_OptionGroupId",
                table: "SKUOptions",
                column: "OptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SKUOptions_OptionId",
                table: "SKUOptions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SKUOptions_SKUId",
                table: "SKUOptions",
                column: "SKUId");
        }
    }
}
