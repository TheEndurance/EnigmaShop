using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Product_SizeGroup_AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeGroupId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SizeGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeGroupId",
                table: "Products",
                column: "SizeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SizeGroup_SizeGroupId",
                table: "Products",
                column: "SizeGroupId",
                principalTable: "SizeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SizeGroup_SizeGroupId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SizeGroup");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeGroupId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SizeGroupId",
                table: "Products");
        }
    }
}
