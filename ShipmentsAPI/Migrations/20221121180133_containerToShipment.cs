using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class containerToShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerNumber",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "ContainerSealNumber",
                table: "Forwarders");

            migrationBuilder.DropColumn(
                name: "ContainerType",
                table: "Forwarders");

            migrationBuilder.AddColumn<string>(
                name: "ContainerNumber",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerSealNumber",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerType",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerNumber",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ContainerSealNumber",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ContainerType",
                table: "Shipments");

            migrationBuilder.AddColumn<string>(
                name: "ContainerNumber",
                table: "Forwarders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerSealNumber",
                table: "Forwarders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContainerType",
                table: "Forwarders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
