using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class optionalWarehouseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseAreaId",
                table: "Shipments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId",
                principalTable: "WarehouseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseAreaId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_WarehouseAreas_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId",
                principalTable: "WarehouseAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
