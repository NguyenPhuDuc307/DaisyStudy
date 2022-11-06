using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyStudy.Data.Migrations
{
    public partial class UpdateSubmission1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionName",
                table: "Submissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmissionName",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
