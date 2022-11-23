using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class POCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PurchaseOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "PurchaseOrders");
        }
    }
}
