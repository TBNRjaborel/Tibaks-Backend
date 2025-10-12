using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tibaks_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "UpdatedAt",
                table: "Children",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Children",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Children");
        }
    }
}
