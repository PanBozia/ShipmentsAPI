using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class CountryOfCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryAddress",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryAddress",
                table: "Customers");
        }
    }
}
