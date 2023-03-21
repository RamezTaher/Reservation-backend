using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class adddistributionpointentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistributionPointId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DistributionPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionPoints", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistributionPointId",
                table: "Users",
                column: "DistributionPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DistributionPoints_DistributionPointId",
                table: "Users",
                column: "DistributionPointId",
                principalTable: "DistributionPoints",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DistributionPoints_DistributionPointId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DistributionPoints");

            migrationBuilder.DropIndex(
                name: "IX_Users_DistributionPointId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DistributionPointId",
                table: "Users");
        }
    }
}
