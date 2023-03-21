using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class pilgrim_trip_optional_housing_contract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilgrimsTrips_HousingContracts_HousingContractId",
                table: "PilgrimsTrips");

            migrationBuilder.AlterColumn<int>(
                name: "HousingContractId",
                table: "PilgrimsTrips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PilgrimsTrips_HousingContracts_HousingContractId",
                table: "PilgrimsTrips",
                column: "HousingContractId",
                principalTable: "HousingContracts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilgrimsTrips_HousingContracts_HousingContractId",
                table: "PilgrimsTrips");

            migrationBuilder.AlterColumn<int>(
                name: "HousingContractId",
                table: "PilgrimsTrips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PilgrimsTrips_HousingContracts_HousingContractId",
                table: "PilgrimsTrips",
                column: "HousingContractId",
                principalTable: "HousingContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
