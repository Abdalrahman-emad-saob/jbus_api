using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SmartMistakeInPassengerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Passengers_FriendId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_FriendId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Passengers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Passengers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FriendId",
                table: "Passengers",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Passengers_FriendId",
                table: "Passengers",
                column: "FriendId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
