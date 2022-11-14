using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_WarehouseArea_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseArea",
                table: "WarehouseArea");

            migrationBuilder.RenameTable(
                name: "WarehouseArea",
                newName: "WarehouseAreas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseAreas",
                table: "WarehouseAreas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId",
                principalTable: "WarehouseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseAreas",
                table: "WarehouseAreas");

            migrationBuilder.RenameTable(
                name: "WarehouseAreas",
                newName: "WarehouseArea");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseArea",
                table: "WarehouseArea",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_WarehouseArea_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId",
                principalTable: "WarehouseArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
