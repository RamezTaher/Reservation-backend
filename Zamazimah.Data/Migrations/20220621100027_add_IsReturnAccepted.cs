using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_IsReturnAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturnAccepted",
                table: "DistributionCycleHousingContracts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturnAccepted",
                table: "DistributionCycleHousingContracts");
        }
    }
}
