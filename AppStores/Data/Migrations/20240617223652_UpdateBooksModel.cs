using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppStores.Data.Migrations
{
    public partial class UpdateBooksModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avtor",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avtor",
                table: "Books");
        }
    }
}
