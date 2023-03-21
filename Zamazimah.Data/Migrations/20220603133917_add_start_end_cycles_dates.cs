using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_start_end_cycles_dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DistributionCompletedDate",
                table: "PilgrimsTrips",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndCycleDate",
                table: "DistributionCycles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartCycleDate",
                table: "DistributionCycles",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptanceDate",
                table: "DistributionCycleHousingContracts",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributionCompletedDate",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "EndCycleDate",
                table: "DistributionCycles");

            migrationBuilder.DropColumn(
                name: "StartCycleDate",
                table: "DistributionCycles");

            migrationBuilder.DropColumn(
                name: "AcceptanceDate",
                table: "DistributionCycleHousingContracts");
        }
    }
}
