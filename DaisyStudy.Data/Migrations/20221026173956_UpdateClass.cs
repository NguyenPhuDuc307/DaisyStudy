using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaisyStudy.Data.Migrations
{
    public partial class UpdateClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Classes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
