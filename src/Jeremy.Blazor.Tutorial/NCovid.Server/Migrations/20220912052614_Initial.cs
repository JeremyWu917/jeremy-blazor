using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NCovid.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    No = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PictureUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailyHealths",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    HealthCondition = table.Column<int>(type: "INTEGER", nullable: false),
                    Temperature = table.Column<float>(type: "REAL", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyHealths", x => new { x.EmployeeId, x.Date });
                    table.ForeignKey(
                        name: "FK_DailyHealths_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "R&D" },
                    { 2, "Market" },
                    { 3, "IT" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "DepartmentId", "Gender", "Name", "No", "PictureUrl" },
                values: new object[,]
                {
                    { 1, new DateTime(1992, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Katherine", "001", "" },
                    { 2, new DateTime(1993, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "Demon", "002", "" },
                    { 3, new DateTime(1999, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Tina", "003", "" },
                    { 4, new DateTime(1987, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Amenda", "004", "" },
                    { 5, new DateTime(1991, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Talor", "005", "" },
                    { 6, new DateTime(1998, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Jerferry", "006", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyHealths");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
