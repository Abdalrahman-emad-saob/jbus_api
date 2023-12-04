using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class RelcoatedFacebookGoogleTokenToPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserGender",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastActive",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PickUpPointId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTransactionId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FinishedAt",
                table: "Trips",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "DropOffPointId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Trips",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "WaypointsReturning",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WaypointsGoing",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Routes",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPointId",
                table: "Routes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndingPointId",
                table: "Routes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Routes",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Routes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Photos",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "TripPickupId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "TripDropoffId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Photos",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Photos",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "InterestPointId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritePointId",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Photos",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Photos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeStamp",
                table: "PaymentTransactions",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PaymentTransactions",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<double>(
                name: "Wallet",
                table: "Passengers",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Passengers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoogleToken",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacebookToken",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Passengers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Passengers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "OTPs",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "OTPs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Otp",
                table: "OTPs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "OTPs",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OTPs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "InterestPoints",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "RouteStartId",
                table: "InterestPoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RouteEndId",
                table: "InterestPoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InterestPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "InterestPoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "InterestPoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "InterestPoints",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InterestPoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "FavoritePoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PointId",
                table: "FavoritePoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "FavoritePoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "FavoritePoints",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FavoritePoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Drivers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Drivers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Drivers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeStamp",
                table: "ChargingTransactions",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "ChargingTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "ChargingTransactions",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChargingTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Buses",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Buses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Buses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Buses",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Buses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "BusNumber",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Buses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserGender",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastActive",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PickUpPointId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTransactionId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FinishedAt",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "DropOffPointId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Trips",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Trips",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "WaypointsReturning",
                table: "Routes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WaypointsGoing",
                table: "Routes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPointId",
                table: "Routes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Routes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EndingPointId",
                table: "Routes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Routes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Routes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "TripPickupId",
                table: "Photos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TripDropoffId",
                table: "Photos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Photos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Photos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Photos",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "InterestPointId",
                table: "Photos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritePointId",
                table: "Photos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Photos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Photos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "PaymentTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeStamp",
                table: "PaymentTransactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "PaymentTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PaymentTransactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PaymentTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<double>(
                name: "Wallet",
                table: "Passengers",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Passengers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Passengers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImage",
                table: "Passengers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoogleToken",
                table: "Passengers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FacebookToken",
                table: "Passengers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Passengers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Passengers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "OTPs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "OTPs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Otp",
                table: "OTPs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "OTPs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OTPs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "InterestPoints",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "RouteStartId",
                table: "InterestPoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RouteEndId",
                table: "InterestPoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "InterestPoints",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "InterestPoints",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "InterestPoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "InterestPoints",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "InterestPoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PointId",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "FavoritePoints",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FavoritePoints",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Drivers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Drivers",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "BusId",
                table: "Drivers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Drivers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "TimeStamp",
                table: "ChargingTransactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "ChargingTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "ChargingTransactions",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ChargingTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "RouteId",
                table: "Buses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Buses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Buses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Buses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BusNumber",
                table: "Buses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Buses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
