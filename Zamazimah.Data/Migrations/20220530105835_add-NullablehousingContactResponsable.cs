using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class addNullablehousingContactResponsable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "HousingContracts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "HousingContracts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Users_ApplicationUserId",
                table: "HousingContracts",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
