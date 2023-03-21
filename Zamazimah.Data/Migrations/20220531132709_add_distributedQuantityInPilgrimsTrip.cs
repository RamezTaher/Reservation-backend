using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_distributedQuantityInPilgrimsTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistributedQuantity",
                table: "PilgrimsTrips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OverDistributionReason",
                table: "PilgrimsTrips",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributedQuantity",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "OverDistributionReason",
                table: "PilgrimsTrips");
        }
    }
}
