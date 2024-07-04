using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaDoMarApi.Migrations
{
    public partial class ForeignKeyRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Financials_FinancialStatus_FinancialStatusId",
                table: "Financials");

            migrationBuilder.DropForeignKey(
                name: "FK_Financials_Orders_OrderId",
                table: "Financials");

            migrationBuilder.DropIndex(
                name: "IX_Financials_FinancialStatusId",
                table: "Financials");

            migrationBuilder.DropIndex(
                name: "IX_Financials_OrderId",
                table: "Financials");

            migrationBuilder.DropColumn(
                name: "FinancialStatusId",
                table: "Financials");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Financials");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Financials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Financials");

            migrationBuilder.AddColumn<int>(
                name: "FinancialStatusId",
                table: "Financials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Financials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Financials_FinancialStatusId",
                table: "Financials",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Financials_OrderId",
                table: "Financials",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Financials_FinancialStatus_FinancialStatusId",
                table: "Financials",
                column: "FinancialStatusId",
                principalTable: "FinancialStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Financials_Orders_OrderId",
                table: "Financials",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
