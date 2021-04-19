using Microsoft.EntityFrameworkCore.Migrations;

namespace geesRecorderApi.Migrations
{
    public partial class ApplicationUserPin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pin",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pin",
                table: "AspNetUsers");
        }
    }
}
