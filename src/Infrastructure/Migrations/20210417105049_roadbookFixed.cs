using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class roadbookFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoadBookEntry_RoadBookEntry_RoadBookEntriesId",
                table: "EmployeeRoadBookEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadBookEntry_Vehicles_VehicleId",
                table: "RoadBookEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoadBookEntry",
                table: "RoadBookEntry");

            migrationBuilder.RenameTable(
                name: "RoadBookEntry",
                newName: "RoadBookEntries");

            migrationBuilder.RenameIndex(
                name: "IX_RoadBookEntry_VehicleId",
                table: "RoadBookEntries",
                newName: "IX_RoadBookEntries_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoadBookEntries",
                table: "RoadBookEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoadBookEntry_RoadBookEntries_RoadBookEntriesId",
                table: "EmployeeRoadBookEntry",
                column: "RoadBookEntriesId",
                principalTable: "RoadBookEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadBookEntries_Vehicles_VehicleId",
                table: "RoadBookEntries",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoadBookEntry_RoadBookEntries_RoadBookEntriesId",
                table: "EmployeeRoadBookEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadBookEntries_Vehicles_VehicleId",
                table: "RoadBookEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoadBookEntries",
                table: "RoadBookEntries");

            migrationBuilder.RenameTable(
                name: "RoadBookEntries",
                newName: "RoadBookEntry");

            migrationBuilder.RenameIndex(
                name: "IX_RoadBookEntries_VehicleId",
                table: "RoadBookEntry",
                newName: "IX_RoadBookEntry_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoadBookEntry",
                table: "RoadBookEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoadBookEntry_RoadBookEntry_RoadBookEntriesId",
                table: "EmployeeRoadBookEntry",
                column: "RoadBookEntriesId",
                principalTable: "RoadBookEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadBookEntry_Vehicles_VehicleId",
                table: "RoadBookEntry",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
