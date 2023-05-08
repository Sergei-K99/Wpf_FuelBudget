using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelBudget.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasuringPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentButgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    GetAllPlanCost = table.Column<double>(type: "REAL", nullable: false),
                    GetAllFactCost = table.Column<double>(type: "REAL", nullable: false),
                    MeasuringPointId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentButgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentButgets_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentButgets_MeasuringPoints_MeasuringPointId",
                        column: x => x.MeasuringPointId,
                        principalTable: "MeasuringPoints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FuelDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuelId = table.Column<int>(type: "INTEGER", nullable: true),
                    FuelPlanCost = table.Column<double>(type: "REAL", nullable: false),
                    FuelFactCost = table.Column<double>(type: "REAL", nullable: false),
                    VolumePlan = table.Column<double>(type: "REAL", nullable: false),
                    VolumeFact = table.Column<double>(type: "REAL", nullable: false),
                    DepartmentButgetId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelDetails_DepartmentButgets_DepartmentButgetId",
                        column: x => x.DepartmentButgetId,
                        principalTable: "DepartmentButgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FuelDetails_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentButgets_DepartmentId",
                table: "DepartmentButgets",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentButgets_MeasuringPointId",
                table: "DepartmentButgets",
                column: "MeasuringPointId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelDetails_DepartmentButgetId",
                table: "FuelDetails",
                column: "DepartmentButgetId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelDetails_FuelId",
                table: "FuelDetails",
                column: "FuelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelDetails");

            migrationBuilder.DropTable(
                name: "DepartmentButgets");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "MeasuringPoints");
        }
    }
}
