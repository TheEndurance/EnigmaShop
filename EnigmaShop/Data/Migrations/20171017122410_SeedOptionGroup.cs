using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EnigmaShop.Data.Migrations
{
    public partial class SeedOptionGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[OptionGroup] ON");
            migrationBuilder.Sql("INSERT INTO[dbo].[OptionGroup]([Id], [Name]) VALUES(1, N'Colour')");
            migrationBuilder.Sql("INSERT INTO[dbo].[OptionGroup] ([Id], [Name]) VALUES(2, N'Size')");
            migrationBuilder.Sql("SET IDENTITY_INSERT[dbo].[OptionGroup] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[OptionGroup] WHERE [Id] = 1");
            migrationBuilder.Sql("DELETE FROM [dbo].[OptionGroup] WHERE [Id] = 2");
        }
    }
}
