using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_center_entiy_add_house_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "HousingContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImportedFromZamazimahDB",
                table: "HousingContracts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HousingContracts_CenterId",
                table: "HousingContracts",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_Centers_CenterId",
                table: "HousingContracts",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_Centers_CenterId",
                table: "HousingContracts");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropIndex(
                name: "IX_HousingContracts_CenterId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "IsImportedFromZamazimahDB",
                table: "HousingContracts");
        }
    }
}
