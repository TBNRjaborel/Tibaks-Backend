using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tibaks_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TetanusToxoidDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Mother_TetanusToxoidDate",
                table: "Children",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mother_TetanusToxoidDate",
                table: "Children");
        }
    }
}
