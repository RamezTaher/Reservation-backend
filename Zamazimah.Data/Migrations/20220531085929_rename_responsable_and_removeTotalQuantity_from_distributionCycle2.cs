using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class rename_responsable_and_removeTotalQuantity_from_distributionCycle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DistributionCycles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DistributionCycles",
                type: "integer",
                nullable: false,
                defaultValue: 0);



        }
    }
}
