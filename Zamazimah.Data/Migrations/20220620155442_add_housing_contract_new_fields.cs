using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_housing_contract_new_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "HousingContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResidencePermitNumber",
                table: "HousingContracts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HousingContracts_CityId",
                table: "HousingContracts",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Cities_CityId",
                table: "HousingContracts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Cities_CityId",
                table: "HousingContracts");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_HousingContracts_CityId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "ResidencePermitNumber",
                table: "HousingContracts");
        }
    }
}
