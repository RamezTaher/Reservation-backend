using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_wassil_number : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WassilNumber",
                table: "HousingContracts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WassilNumber",
                table: "HousingContracts");
        }
    }
}
