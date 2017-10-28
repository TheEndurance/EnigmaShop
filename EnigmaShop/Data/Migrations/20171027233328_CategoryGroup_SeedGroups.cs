using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class CategoryGroup_SeedGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[CategoryGroups] VALUES (N'Men')");
            migrationBuilder.Sql("INSERT INTO [dbo].[CategoryGroups] VALUES (N'Women')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[CategoryGroups] WHERE Type = N'Men'");
            migrationBuilder.Sql("DELETE FROM [dbo].[CategoryGroups] WHERE Type = N'Women'");
            
        }
    }
}
