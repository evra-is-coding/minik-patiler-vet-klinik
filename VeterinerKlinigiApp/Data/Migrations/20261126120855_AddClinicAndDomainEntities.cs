using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinerKlinigiApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClinicAndDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicServices_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veterinarians_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Veterinarians_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccineRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    VaccineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdministeredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRecords_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    VeterinarianId = table.Column<int>(type: "int", nullable: false),
                    ClinicServiceId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExaminationNote = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_ClinicServices_ClinicServiceId",
                        column: x => x.ClinicServiceId,
                        principalTable: "ClinicServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VeterinarianId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianAvailabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinarianAvailabilities_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeterinarianServices",
                columns: table => new
                {
                    VeterinarianId = table.Column<int>(type: "int", nullable: false),
                    ClinicServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinarianServices", x => new { x.VeterinarianId, x.ClinicServiceId });
                    table.ForeignKey(
                        name: "FK_VeterinarianServices_ClinicServices_ClinicServiceId",
                        column: x => x.ClinicServiceId,
                        principalTable: "ClinicServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VeterinarianServices_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClinicServiceId",
                table: "Appointments",
                column: "ClinicServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId",
                table: "Appointments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VeterinarianId",
                table: "Appointments",
                column: "VeterinarianId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicServices_ClinicId",
                table: "ClinicServices",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRecords_PetId",
                table: "VaccineRecords",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianAvailabilities_VeterinarianId",
                table: "VeterinarianAvailabilities",
                column: "VeterinarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_ClinicId",
                table: "Veterinarians",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_UserId",
                table: "Veterinarians",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinarianServices_ClinicServiceId",
                table: "VeterinarianServices",
                column: "ClinicServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "VaccineRecords");

            migrationBuilder.DropTable(
                name: "VeterinarianAvailabilities");

            migrationBuilder.DropTable(
                name: "VeterinarianServices");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "ClinicServices");

            migrationBuilder.DropTable(
                name: "Veterinarians");

            migrationBuilder.DropTable(
                name: "Clinics");
        }
    }
}
