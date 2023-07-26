using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelEasy.EV.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class fkmodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectricVehicles_Bookings_BookingId",
                table: "ElectricVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ElectricVehicles_BookingId",
                table: "ElectricVehicles");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "ElectricVehicles");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ElectricVehicleId",
                table: "Bookings",
                column: "ElectricVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ElectricVehicles_ElectricVehicleId",
                table: "Bookings",
                column: "ElectricVehicleId",
                principalTable: "ElectricVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ElectricVehicles_ElectricVehicleId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ElectricVehicleId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "ElectricVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ElectricVehicles_BookingId",
                table: "ElectricVehicles",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectricVehicles_Bookings_BookingId",
                table: "ElectricVehicles",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
