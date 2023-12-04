using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class EditedPasswordAlgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserGender",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "bytea",
                nullable: true);
        }
    }
}
