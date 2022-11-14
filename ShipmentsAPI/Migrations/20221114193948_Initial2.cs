using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPriority",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<bool>(
                name: "HasPriority",
                table: "Shipments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPriority",
                table: "Shipments");

            migrationBuilder.AddColumn<bool>(
                name: "HasPriority",
                table: "PurchaseOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
