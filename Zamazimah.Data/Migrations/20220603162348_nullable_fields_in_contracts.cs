using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class nullable_fields_in_contracts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "ActualQuantity",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ReservedQuantity",
                table: "Stores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "HousingContracts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "LocationNatureId",
                table: "HousingContracts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "HousingContracts",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts",
                column: "LocationNatureId",
                principalTable: "LocationNatures",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts");

            migrationBuilder.AddColumn<int>(
                name: "ActualQuantity",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservedQuantity",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "HousingContracts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationNatureId",
                table: "HousingContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "HousingContracts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts",
                column: "LocationNatureId",
                principalTable: "LocationNatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
