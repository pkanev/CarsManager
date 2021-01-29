using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsManager.Infrastructure.Migrations
{
    public partial class indexedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Models_MakeId",
                table: "Models");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId_Name",
                table: "Models",
                columns: new[] { "MakeId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Models_MakeId_Name",
                table: "Models");

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");
        }
    }
}
