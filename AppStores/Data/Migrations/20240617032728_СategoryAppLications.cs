using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppStores.Data.Migrations
{
    public partial class СategoryAppLications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Applications");
        }
    }
}
