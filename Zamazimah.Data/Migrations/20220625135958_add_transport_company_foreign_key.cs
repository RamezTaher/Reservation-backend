using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_transport_company_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportCompanyId",
                table: "PilgrimsTrips",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PilgrimsTrips_TransportCompanyId",
                table: "PilgrimsTrips",
                column: "TransportCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PilgrimsTrips_TransportCompanies_TransportCompanyId",
                table: "PilgrimsTrips",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilgrimsTrips_TransportCompanies_TransportCompanyId",
                table: "PilgrimsTrips");

            migrationBuilder.DropIndex(
                name: "IX_PilgrimsTrips_TransportCompanyId",
                table: "PilgrimsTrips");

            migrationBuilder.DropColumn(
                name: "TransportCompanyId",
                table: "PilgrimsTrips");
        }
    }
}
