using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class rename_housingcontract_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionCycleHousingContracts_HousingContracts_Hounsing~",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.RenameColumn(
                name: "HounsingContractId",
                table: "DistributionCycleHousingContracts",
                newName: "HousingContractId");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionCycleHousingContracts_HounsingContractId",
                table: "DistributionCycleHousingContracts",
                newName: "IX_DistributionCycleHousingContracts_HousingContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionCycleHousingContracts_HousingContracts_HousingC~",
                table: "DistributionCycleHousingContracts",
                column: "HousingContractId",
                principalTable: "HousingContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionCycleHousingContracts_HousingContracts_HousingC~",
                table: "DistributionCycleHousingContracts");

            migrationBuilder.RenameColumn(
                name: "HousingContractId",
                table: "DistributionCycleHousingContracts",
                newName: "HounsingContractId");

            migrationBuilder.RenameIndex(
                name: "IX_DistributionCycleHousingContracts_HousingContractId",
                table: "DistributionCycleHousingContracts",
                newName: "IX_DistributionCycleHousingContracts_HounsingContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionCycleHousingContracts_HousingContracts_Hounsing~",
                table: "DistributionCycleHousingContracts",
                column: "HounsingContractId",
                principalTable: "HousingContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
