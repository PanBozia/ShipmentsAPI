using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class ChangePallQty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PalletQty",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "PalletQty",
                table: "Shipments",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PalletQty",
                table: "Shipments");

            migrationBuilder.AddColumn<int>(
                name: "PalletQty",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);
        }
    }
}
