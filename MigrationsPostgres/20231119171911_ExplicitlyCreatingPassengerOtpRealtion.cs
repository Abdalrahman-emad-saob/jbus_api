using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class ExplicitlyCreatingPassengerOtpRealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
