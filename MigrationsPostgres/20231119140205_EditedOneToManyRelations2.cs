using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class EditedOneToManyRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingTransactions_Passengers_PassengerId",
                table: "ChargingTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Passengers_PassengerId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Passengers_PassengerId",
                table: "Trips");

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
                name: "PassengerId",
                table: "OTPs",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingTransactions_Passengers_PassengerId",
                table: "ChargingTransactions",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Passengers_PassengerId",
                table: "PaymentTransactions",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Passengers_PassengerId",
                table: "Trips",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_ChargingTransactions_Passengers_PassengerId",
                table: "ChargingTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Passengers_PassengerId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Passengers_PassengerId",
                table: "Trips");

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
                name: "PassengerId",
                table: "OTPs",
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
                name: "FK_Buses_Routes_RouteId",
                table: "Buses",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargingTransactions_Passengers_PassengerId",
                table: "ChargingTransactions",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Passengers_PassengerId",
                table: "OTPs",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Passengers_PassengerId",
                table: "PaymentTransactions",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId",
                table: "Trips",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Passengers_PassengerId",
                table: "Trips",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");
        }
    }
}
