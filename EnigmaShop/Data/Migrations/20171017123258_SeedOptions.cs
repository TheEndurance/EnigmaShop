using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SeedOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Red', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Blue', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Green', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Yellow', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Purple', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'Black', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'White', 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'S', 2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'M', 2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'L', 2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Options] ([Name], [OptionGroupId]) VALUES (N'XL', 2)");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'Red'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'Green'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'Yellow'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'Purple'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'Black'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'White'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'S'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'M'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'L'");
            migrationBuilder.Sql("DELETE FROM [dbo].[Options] WHERE [Name] = N'XL'");
        }
    }
}
