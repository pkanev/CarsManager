using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class UniqueLicencePlate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicencePlate",
                table: "Vehicles",
                column: "LicencePlate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_LicencePlate",
                table: "Vehicles");
        }
    }
}
