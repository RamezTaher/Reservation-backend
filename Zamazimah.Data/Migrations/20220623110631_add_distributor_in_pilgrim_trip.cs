using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_distributor_in_pilgrim_trip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistributorId",
                table: "PilgrimsTrips",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PilgrimsTrips_DistributorId",
                table: "PilgrimsTrips",
                column: "DistributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PilgrimsTrips_Users_DistributorId",
                table: "PilgrimsTrips",
                column: "DistributorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilgrimsTrips_Users_DistributorId",
                table: "PilgrimsTrips");

            migrationBuilder.DropIndex(
                name: "IX_PilgrimsTrips_DistributorId",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "PilgrimsTrips");
        }
    }
}
