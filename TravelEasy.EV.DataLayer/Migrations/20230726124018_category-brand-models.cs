using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelEasy.EV.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class categorybrandmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "ElectricVehicles");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ElectricVehicles");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "ElectricVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ElectricVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVehicles_BrandId",
                table: "ElectricVehicles",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVehicles_CategoryId",
                table: "ElectricVehicles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectricVehicles_Brand_BrandId",
                table: "ElectricVehicles",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElectricVehicles_Category_CategoryId",
                table: "ElectricVehicles",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectricVehicles_Brand_BrandId",
                table: "ElectricVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_ElectricVehicles_Category_CategoryId",
                table: "ElectricVehicles");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_ElectricVehicles_BrandId",
                table: "ElectricVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ElectricVehicles_CategoryId",
                table: "ElectricVehicles");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "ElectricVehicles");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ElectricVehicles");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "ElectricVehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ElectricVehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
