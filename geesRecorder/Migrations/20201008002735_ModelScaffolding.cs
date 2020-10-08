using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using geesRecorder.Models;

namespace geesRecorder.Migrations
{
    public partial class ModelScaffolding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EventTracker = table.Column<ICollection<AttendanceEvent>>(type: "jsonb", nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataCollations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TextFieldsSchema = table.Column<string>(nullable: true),
                    NumberFieldsSchema = table.Column<string>(nullable: true),
                    SelectionFieldsSchema = table.Column<string>(nullable: true),
                    Records = table.Column<ICollection<Record>>(type: "jsonb", nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCollations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCollations_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendant",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Events = table.Column<ICollection<AttendanceEvent>>(type: "jsonb", nullable: true),
                    AttendanceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendant_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ApplicationUserId",
                table: "Attendances",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendant_AttendanceId",
                table: "Attendant",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataCollations_ApplicationUserId",
                table: "DataCollations",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendant");

            migrationBuilder.DropTable(
                name: "DataCollations");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
