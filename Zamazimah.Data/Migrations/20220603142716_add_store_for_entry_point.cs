﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_store_for_entry_point : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForEntryPoint",
                table: "Stores",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForEntryPoint",
                table: "Stores");
        }
    }
}
