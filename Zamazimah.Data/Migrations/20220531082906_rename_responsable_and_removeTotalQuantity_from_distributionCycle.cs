using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class rename_responsable_and_removeTotalQuantity_from_distributionCycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "HousingContracts",
                newName: "ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_HousingContracts_ApplicationUserId",
                table: "HousingContracts",
                newName: "IX_HousingContracts_ResponsableId");



            migrationBuilder.AddColumn<string>(
                name: "StoreId",
                table: "DistributionCycles",
                type: "integer",
                nullable: true);



            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Users_ResponsableId",
                table: "HousingContracts",
                column: "ResponsableId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Users_ResponsableId",
                table: "HousingContracts");

   

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DistributionCycles");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "HousingContracts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_HousingContracts_ResponsableId",
                table: "HousingContracts",
                newName: "IX_HousingContracts_ApplicationUserId");



            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
