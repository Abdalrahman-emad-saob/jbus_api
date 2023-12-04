using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace datamigrations
{
    /// <inheritdoc />
    public partial class AddedManyRelationsSomeRemaining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    Wallet = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    FavoritePointId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    WaypointsGoing = table.Column<string>(type: "text", nullable: true),
                    WaypointsReturning = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargingTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassengerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargingTransactions_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Otp = table.Column<int>(type: "integer", nullable: false),
                    PassengerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTPs_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoritePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PassengerId = table.Column<int>(type: "integer", nullable: false),
                    PointId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritePoints_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoritePoints_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusNumber = table.Column<string>(type: "text", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    RouteId = table.Column<int>(type: "integer", nullable: false),
                    DriverId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buses_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buses_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterestPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    StartingPointId = table.Column<int>(type: "integer", nullable: false),
                    EndingPointId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestPoints_Points_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestPoints_Routes_EndingPointId",
                        column: x => x.EndingPointId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestPoints_Routes_StartingPointId",
                        column: x => x.StartingPointId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    PassengerId = table.Column<int>(type: "integer", nullable: false),
                    BusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PassengerId = table.Column<int>(type: "integer", nullable: false),
                    TripId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DriverId",
                table: "Buses",
                column: "DriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_RouteId",
                table: "Buses",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargingTransactions_PassengerId",
                table: "ChargingTransactions",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserId",
                table: "Drivers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePoints_PassengerId",
                table: "FavoritePoints",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePoints_PointId",
                table: "FavoritePoints",
                column: "PointId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_PassengerId",
                table: "OTPs",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_UserId",
                table: "Passengers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PassengerId",
                table: "PaymentTransactions",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_TripId",
                table: "PaymentTransactions",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BusId",
                table: "Trips",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PassengerId",
                table: "Trips",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingTransactions");

            migrationBuilder.DropTable(
                name: "FavoritePoints");

            migrationBuilder.DropTable(
                name: "InterestPoints");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Users");
        }
    }
}
