using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class RoadBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntConstants");

            migrationBuilder.CreateTable(
                name: "RoadBookEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CheckedOut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CheckedIn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    OldMileage = table.Column<int>(type: "integer", nullable: false),
                    NewMileage = table.Column<int>(type: "integer", nullable: false),
                    Destination = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadBookEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadBookEntry_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoadBookEntry",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "integer", nullable: false),
                    RoadBookEntriesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoadBookEntry", x => new { x.EmployeesId, x.RoadBookEntriesId });
                    table.ForeignKey(
                        name: "FK_EmployeeRoadBookEntry_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRoadBookEntry_RoadBookEntry_RoadBookEntriesId",
                        column: x => x.RoadBookEntriesId,
                        principalTable: "RoadBookEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoadBookEntry_RoadBookEntriesId",
                table: "EmployeeRoadBookEntry",
                column: "RoadBookEntriesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadBookEntry_VehicleId",
                table: "RoadBookEntry",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoadBookEntry");

            migrationBuilder.DropTable(
                name: "RoadBookEntry");

            migrationBuilder.CreateTable(
                name: "IntConstants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntConstants", x => x.Id);
                });
        }
    }
}
