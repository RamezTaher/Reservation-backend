using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_centers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommercialHousingName",
                table: "HousingContracts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CenterId",
                table: "Users",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Centers_CenterId",
                table: "Users",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Centers_CenterId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CenterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommercialHousingName",
                table: "HousingContracts");
        }
    }
}
