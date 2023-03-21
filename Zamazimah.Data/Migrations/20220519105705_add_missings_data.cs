using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zamazimah.Data.Migrations
{
    public partial class add_missings_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationNatureId",
                table: "HousingContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignatureDate",
                table: "DistributionContracts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "LocationNatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationNatures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HousingContracts_LocationNatureId",
                table: "HousingContracts",
                column: "LocationNatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts",
                column: "LocationNatureId",
                principalTable: "LocationNatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HousingContracts_LocationNatures_LocationNatureId",
                table: "HousingContracts");

            migrationBuilder.DropTable(
                name: "LocationNatures");

            migrationBuilder.DropIndex(
                name: "IX_HousingContracts_LocationNatureId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "LocationNatureId",
                table: "HousingContracts");

            migrationBuilder.DropColumn(
                name: "SignatureDate",
                table: "DistributionContracts");
        }
    }
}
