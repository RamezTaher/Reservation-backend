using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class housingContract_latitude_longitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "HousingContracts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "HousingContracts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "HousingContracts");
        }
    }
}
