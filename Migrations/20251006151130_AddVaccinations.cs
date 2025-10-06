using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tibaks_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChildId = table.Column<string>(type: "text", nullable: false),
                    VaccineId = table.Column<int>(type: "integer", nullable: false),
                    BatchLotNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DoseNumber = table.Column<int>(type: "integer", nullable: false),
                    Dosage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateAdministered = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HealthcareWorkerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinations_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vaccinations_HealthcareWorkers_HealthcareWorkerId",
                        column: x => x.HealthcareWorkerId,
                        principalTable: "HealthcareWorkers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vaccinations_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_ChildId",
                table: "Vaccinations",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_HealthcareWorkerId",
                table: "Vaccinations",
                column: "HealthcareWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineId",
                table: "Vaccinations",
                column: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaccinations");
        }
    }
}
