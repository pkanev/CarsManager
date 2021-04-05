using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class VehicleImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Employees",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vehicles",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Employees",
                newName: "ImageName");
        }
    }
}
