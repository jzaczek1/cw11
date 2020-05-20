using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cw11.Migrations
{
    public partial class AddedAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    IdDoctor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Doctor_PK", x => x.IdDoctor);
                });

            migrationBuilder.CreateTable(
                name: "Medicament",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Medicament_PK", x => x.IdMedicament);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    IdPatient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Patient_PK", x => x.IdPatient);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IdPatient = table.Column<int>(nullable: false),
                    IdDoctor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Prescription_PK", x => x.IdPrescription);
                    table.ForeignKey(
                        name: "Prescription_Doctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctor",
                        principalColumn: "IdDoctor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Prescription_Patient",
                        column: x => x.IdPatient,
                        principalTable: "Patient",
                        principalColumn: "IdPatient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescription_Medicament",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(nullable: false),
                    IdPrescription = table.Column<int>(nullable: false),
                    Dose = table.Column<int>(nullable: false),
                    Details = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PM_PK", x => new { x.IdMedicament, x.IdPrescription });
                    table.ForeignKey(
                        name: "PM_Med",
                        column: x => x.IdMedicament,
                        principalTable: "Medicament",
                        principalColumn: "IdMedicament",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PM_Pres",
                        column: x => x.IdPrescription,
                        principalTable: "Prescription",
                        principalColumn: "IdPrescription",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "JanKowalski@amorek.pl", "Jan", "Kowalski" },
                    { 2, "HubertNowak@LuxMed.com", "Hubert", "Nowak" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Na ból głowy", "Xanax", "Słabe leki" },
                    { 2, "Na ból d", "Ibupar", "Mocne leki" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1966, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Byku", "Bykowski" },
                    { 2, new DateTime(1996, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Nowak" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 1, new DateTime(2020, 5, 20, 11, 34, 6, 956, DateTimeKind.Local).AddTicks(6518), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 2, new DateTime(2020, 5, 20, 11, 34, 6, 959, DateTimeKind.Local).AddTicks(5215), new DateTime(2022, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[] { 1, 1, "Brać ile wlezie", 1 });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[] { 2, 2, "Nie więcej niż 1 rocznie", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescription",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescription",
                column: "IdPatient");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_Medicament_IdPrescription",
                table: "Prescription_Medicament",
                column: "IdPrescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescription_Medicament");

            migrationBuilder.DropTable(
                name: "Medicament");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
