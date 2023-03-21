using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class representative2_representative3_in_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ViceEmail2",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViceEmail3",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViceFirstName2",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViceFirstName3",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViceLastName2",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViceLastName3",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VicePhone2",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VicePhone3",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViceEmail2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ViceEmail3",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ViceFirstName2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ViceFirstName3",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ViceLastName2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ViceLastName3",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VicePhone2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VicePhone3",
                table: "Users");
        }
    }
}
