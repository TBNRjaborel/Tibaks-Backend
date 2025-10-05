using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tibaks_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddChildren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ChildInfo_FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ChildInfo_MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ChildInfo_LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ChildInfo_Suffix = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ChildInfo_Sex = table.Column<byte>(type: "smallint", nullable: false),
                    ChildInfo_DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChildInfo_BirthOrder = table.Column<int>(type: "integer", nullable: false),
                    ChildInfo_PlaceOfDelivery = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ChildInfo_BirthWeight = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ChildInfo_FeedingType = table.Column<int>(type: "integer", nullable: false),
                    ChildInfo_DateReferredForNewbornScreening = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChildInfo_DateAssessed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address_HomeAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Address_NearestLandmark = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Mother_Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mother_DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Mother_Occupation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Mother_ContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Mother_TetanusToxoidStatus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mother_PhicNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Father_Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Father_DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Father_Occupation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Father_ContactNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Children");
        }
    }
}
