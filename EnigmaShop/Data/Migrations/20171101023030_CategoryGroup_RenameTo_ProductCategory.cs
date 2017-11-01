using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class CategoryGroup_RenameTo_ProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryGroups_CategoryGroupId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CategoryGroups");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductCategories_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_CategoryGroupId",
                table: "Products",
                column: "CategoryGroupId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductCategories_CategoryGroupId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_CategoryGroupId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.CreateTable(
                name: "CategoryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryGroups", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId",
                table: "Categories",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryGroups_CategoryGroupId",
                table: "Products",
                column: "CategoryGroupId",
                principalTable: "CategoryGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
