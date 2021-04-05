using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class RemoveDurationAndDateFromLiabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Vignettes");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Vignettes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "MOTs");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "MOTs");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CivilLiabilities");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CivilLiabilities");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "CarInsurances");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CarInsurances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Vignettes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Vignettes",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MOTs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "MOTs",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CivilLiabilities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "CivilLiabilities",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "CarInsurances",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "CarInsurances",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
