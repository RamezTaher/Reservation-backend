using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class addhousingContactResponsable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "HousingContracts",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HousingContracts_ApplicationUserId",
                table: "HousingContracts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts");

            migrationBuilder.DropIndex(
                name: "IX_HousingContracts_ApplicationUserId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "HousingContracts");

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
    }
}
