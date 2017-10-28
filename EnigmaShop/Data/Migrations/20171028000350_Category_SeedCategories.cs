using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class Category_SeedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] (Type,CategoryGroupId) VALUES (N'Shirts',1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] (Type,CategoryGroupId) VALUES (N'Shirts',2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] (Type,CategoryGroupId) VALUES (N'Pants',1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] (Type,CategoryGroupId) VALUES (N'Pants',2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories] WHERE Type = N'Shirts' AND CategoryGroupId = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories] WHERE Type = N'Shirts' AND CategoryGroupId = 2");
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories] WHERE Type = N'Pants' AND CategoryGroupId = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories] WHERE Type = N'Pants' AND CategoryGroupId = 2");
        }
    }
}
