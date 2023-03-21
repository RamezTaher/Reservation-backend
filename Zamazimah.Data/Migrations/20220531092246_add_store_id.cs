using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_store_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "DistributionCycles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DistributionCycles_StoreId",
                table: "DistributionCycles",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistributionCycles_Stores_StoreId",
                table: "DistributionCycles",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistributionCycles_Stores_StoreId",
                table: "DistributionCycles");

            migrationBuilder.DropIndex(
                name: "IX_DistributionCycles_StoreId",
                table: "DistributionCycles");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "DistributionCycles");
        }
    }
}
