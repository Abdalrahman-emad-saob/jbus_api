using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class ChangedDateTimeTypeToDateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleToken",
                table: "Users");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "FacebookToken",
                table: "Passengers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleToken",
                table: "Passengers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookToken",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "GoogleToken",
                table: "Passengers");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "FacebookToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleToken",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
