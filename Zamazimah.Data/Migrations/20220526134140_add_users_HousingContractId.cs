using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_users_HousingContractId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionCycleHousingContracts_Users_ResponsableId",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.DropIndex(
                name: "IX_DistributionCycleHousingContracts_ResponsableId",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.DropColumn(
                name: "ResponsableId",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.AddColumn<int>(
                name: "HousingContractId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HousingContractId",
                table: "Users",
                column: "HousingContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HousingContracts_HousingContractId",
                table: "Users",
                column: "HousingContractId",
                principalTable: "HousingContracts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_HousingContracts_HousingContractId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HousingContractId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HousingContractId",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "DistributionCycleHousingContracts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResponsableId",
                table: "DistributionCycleHousingContracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DistributionCycleHousingContracts_ResponsableId",
                table: "DistributionCycleHousingContracts",
                column: "ResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionCycleHousingContracts_Users_ResponsableId",
                table: "DistributionCycleHousingContracts",
                column: "ResponsableId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
