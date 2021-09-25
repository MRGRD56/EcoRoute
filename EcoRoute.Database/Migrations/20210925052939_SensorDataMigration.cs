using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoRoute.Database.Migrations
{
    public partial class SensorDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorDataList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<float>(type: "REAL", nullable: false),
                    Humidity = table.Column<float>(type: "REAL", nullable: false),
                    Co2 = table.Column<float>(type: "REAL", nullable: false),
                    Los = table.Column<float>(type: "REAL", nullable: false),
                    DustPm1 = table.Column<float>(type: "REAL", nullable: false),
                    DustPm25 = table.Column<float>(type: "REAL", nullable: false),
                    DustPm10 = table.Column<float>(type: "REAL", nullable: false),
                    Pressure = table.Column<float>(type: "REAL", nullable: false),
                    Aqi = table.Column<float>(type: "REAL", nullable: false),
                    Formaldehyde = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDataList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDataList_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorDataList_SensorId",
                table: "SensorDataList",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorDataList");

            migrationBuilder.DropTable(
                name: "Sensors");
        }
    }
}
