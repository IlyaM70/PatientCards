using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientCards.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DateOfAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Patronymic", "Phone" },
                values: new object[,]
                {
                    { "10FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateOnly(1976, 10, 11), "Александр", "Морозов", "Васильевич", "+79171223345" },
                    { "11FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateOnly(1986, 9, 9), "Петр", "Романов", "", "+79191223385" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "DateOfAppointment", "Diagnosis", "PatientId" },
                values: new object[,]
                {
                    { "10FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateTime(2024, 1, 21, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1596), "E00.0,Синдром врожденной йодной недостаточности, неврологическая форма", "10FD2873-FF9D-4951-9346-1C71BD0AC0DE" },
                    { "11FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateTime(2024, 1, 31, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1606), "E01.0,Диффузный (эндемический) зоб, связанный с йодной недостаточностью", "10FD2873-FF9D-4951-9346-1C71BD0AC0DE" },
                    { "12FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateTime(2023, 10, 23, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1608), "E10,Сахарный диабет I типа", "11FD2873-FF9D-4951-9346-1C71BD0AC0DE" },
                    { "13FD2873-FF9D-4951-9346-1C71BD0AC0DE", new DateTime(2024, 1, 30, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1611), "E11,Сахарный диабет II типа", "11FD2873-FF9D-4951-9346-1C71BD0AC0DE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
