using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SKUOptionTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKUOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionGroupId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    SKUId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKUOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SKUOptions_OptionGroup_OptionGroupId",
                        column: x => x.OptionGroupId,
                        principalTable: "OptionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SKUOptions_Option_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Option",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SKUOptions");

            migrationBuilder.DropTable(
                name: "OptionGroup");

            migrationBuilder.DropTable(
                name: "Option");
        }
    }
}
