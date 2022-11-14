using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseAreaId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WarehouseArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseArea", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_WarehouseArea_WarehouseAreaId",
                table: "Shipments",
                column: "WarehouseAreaId",
                principalTable: "WarehouseArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_WarehouseArea_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.DropTable(
                name: "WarehouseArea");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_WarehouseAreaId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "WarehouseAreaId",
                table: "Shipments");
        }
    }
}
