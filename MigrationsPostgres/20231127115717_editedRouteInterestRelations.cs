using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class editedRouteInterestRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartingPointId",
                table: "InterestPoints",
                newName: "RouteStartId");

            migrationBuilder.RenameColumn(
                name: "EndingPointId",
                table: "InterestPoints",
                newName: "RouteEndId");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DropOffPointId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PickUpPointId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripDropoffId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripPickupId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_TripDropoffId",
                table: "Photos",
                column: "TripDropoffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_TripPickupId",
                table: "Photos",
                column: "TripPickupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Trips_TripDropoffId",
                table: "Photos",
                column: "TripDropoffId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Trips_TripPickupId",
                table: "Photos",
                column: "TripPickupId",
                principalTable: "Trips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Trips_TripDropoffId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Trips_TripPickupId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_TripDropoffId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_TripPickupId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DropOffPointId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PickUpPointId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripDropoffId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "TripPickupId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "RouteStartId",
                table: "InterestPoints",
                newName: "StartingPointId");

            migrationBuilder.RenameColumn(
                name: "RouteEndId",
                table: "InterestPoints",
                newName: "EndingPointId");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
