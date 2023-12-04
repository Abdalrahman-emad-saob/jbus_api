using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class EditedOTPsPassengerRealtionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Points_PointId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPoints_Points_LocationId",
                table: "InterestPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPoints_Routes_EndingPointId",
                table: "InterestPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPoints_Routes_StartingPointId",
                table: "InterestPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Trips_TripId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_TripId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InterestPoints_EndingPointId",
                table: "InterestPoints");

            migrationBuilder.DropIndex(
                name: "IX_InterestPoints_LocationId",
                table: "InterestPoints");

            migrationBuilder.DropIndex(
                name: "IX_InterestPoints_StartingPointId",
                table: "InterestPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "Photos");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentTransactionId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EndingPointId",
                table: "Routes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartingPointId",
                table: "Routes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Passengers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Passengers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OTPs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OTPs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "InterestPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "InterestPoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FavoritePoints",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "InterestPointId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PointId",
                table: "Trips",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PointId1",
                table: "Trips",
                column: "PointId1");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_EndingPointId",
                table: "Routes",
                column: "EndingPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StartingPointId",
                table: "Routes",
                column: "StartingPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TripId",
                table: "PaymentTransactions",
                column: "TripId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterestPoints_LocationId",
                table: "InterestPoints",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePoints_RouteId",
                table: "FavoritePoints",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Photos_PointId",
                table: "FavoritePoints",
                column: "PointId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPoints_Photos_LocationId",
                table: "InterestPoints",
                column: "LocationId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Trips_TripId",
                table: "PaymentTransactions",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_InterestPoints_EndingPointId",
                table: "Routes",
                column: "EndingPointId",
                principalTable: "InterestPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_InterestPoints_StartingPointId",
                table: "Routes",
                column: "StartingPointId",
                principalTable: "InterestPoints",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_RouteId",
                table: "Trips",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Photos_PointId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePoints_Routes_RouteId",
                table: "FavoritePoints");

            migrationBuilder.DropForeignKey(
                name: "FK_InterestPoints_Photos_LocationId",
                table: "InterestPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Trips_TripId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_InterestPoints_EndingPointId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_InterestPoints_StartingPointId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Photos_PointId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Photos_PointId1",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_RouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PointId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PointId1",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_RouteId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Routes_EndingPointId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_StartingPointId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_TripId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InterestPoints_LocationId",
                table: "InterestPoints");

            migrationBuilder.DropIndex(
                name: "IX_FavoritePoints_RouteId",
                table: "FavoritePoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PaymentTransactionId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PointId1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "EndingPointId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "StartingPointId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InterestPoints");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "InterestPoints");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FavoritePoints");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "FavoritePoints");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Buses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "InterestPointId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Points");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TripId",
                table: "PaymentTransactions",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestPoints_EndingPointId",
                table: "InterestPoints",
                column: "EndingPointId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestPoints_LocationId",
                table: "InterestPoints",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestPoints_StartingPointId",
                table: "InterestPoints",
                column: "StartingPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buses_Routes_RouteId",
                table: "Buses",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePoints_Points_PointId",
                table: "FavoritePoints",
                column: "PointId",
                principalTable: "Points",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPoints_Points_LocationId",
                table: "InterestPoints",
                column: "LocationId",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPoints_Routes_EndingPointId",
                table: "InterestPoints",
                column: "EndingPointId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPoints_Routes_StartingPointId",
                table: "InterestPoints",
                column: "StartingPointId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Trips_TripId",
                table: "PaymentTransactions",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
