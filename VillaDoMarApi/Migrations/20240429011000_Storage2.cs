using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaDoMarApi.Migrations
{
    public partial class Storage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Storage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Storage");
        }
    }
}
