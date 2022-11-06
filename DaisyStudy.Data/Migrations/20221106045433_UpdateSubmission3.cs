using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyStudy.Data.Migrations
{
    public partial class UpdateSubmission3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeUpdated",
                table: "Submissions",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeUpdated",
                table: "Submissions");
        }
    }
}
