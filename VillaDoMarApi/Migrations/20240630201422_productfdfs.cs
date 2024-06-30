using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaDoMarApi.Migrations
{
    public partial class productfdfs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SuppliersProducts_SupplierProductIdId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierProductIdId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeProduct",
                table: "Products",
                newName: "TypeProductId");

            migrationBuilder.AddColumn<int>(
                name: "SupplierProductId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierProductId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeProductId",
                table: "Products",
                newName: "TypeProduct");

            migrationBuilder.AddColumn<int>(
                name: "SupplierProductIdId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierProductIdId",
                table: "Products",
                column: "SupplierProductIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SuppliersProducts_SupplierProductIdId",
                table: "Products",
                column: "SupplierProductIdId",
                principalTable: "SuppliersProducts",
                principalColumn: "Id");
        }
    }
}
