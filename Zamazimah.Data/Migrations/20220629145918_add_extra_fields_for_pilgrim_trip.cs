using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_extra_fields_for_pilgrim_trip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PilgrimsTrips",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TripImage",
                table: "PilgrimsTrips",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PilgrimsTrips",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "TripImage",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PilgrimsTrips");
        }
    }
}
