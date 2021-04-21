using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class AddedActiveRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedEmployeesCount",
                table: "RoadBookEntries");

            migrationBuilder.CreateTable(
                name: "EmployeeRoadBookEntry1",
                columns: table => new
                {
                    ActiveRecordsId = table.Column<int>(type: "integer", nullable: false),
                    ActiveUsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoadBookEntry1", x => new { x.ActiveRecordsId, x.ActiveUsersId });
                    table.ForeignKey(
                        name: "FK_EmployeeRoadBookEntry1_Employees_ActiveUsersId",
                        column: x => x.ActiveUsersId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRoadBookEntry1_RoadBookEntries_ActiveRecordsId",
                        column: x => x.ActiveRecordsId,
                        principalTable: "RoadBookEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoadBookEntry1_ActiveUsersId",
                table: "EmployeeRoadBookEntry1",
                column: "ActiveUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoadBookEntry1");

            migrationBuilder.AddColumn<int>(
                name: "AssignedEmployeesCount",
                table: "RoadBookEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
