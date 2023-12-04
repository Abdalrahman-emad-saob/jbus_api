using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class EditedOneToManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Passengers_PassengerId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Photos_PointId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Photos_PointId1",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PointId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PointId1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PointId1",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Trips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Trips",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "PaymentTransactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "FavoritePoints",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "ChargingTransactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Buses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Passengers_PassengerId",
                table: "FavoritePoints",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Passengers_PassengerId",
                table: "FavoritePoints");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointId",
                table: "Trips",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PointId1",
                table: "Trips",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "PaymentTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "ChargingTransactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Buses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PointId",
                table: "Trips",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PointId1",
                table: "Trips",
                column: "PointId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Passengers_PassengerId",
                table: "FavoritePoints",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Photos_PointId",
                table: "Trips",
                column: "PointId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Photos_PointId1",
                table: "Trips",
                column: "PointId1",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
