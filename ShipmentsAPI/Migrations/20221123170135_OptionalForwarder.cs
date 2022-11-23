using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class OptionalForwarder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Forwarders_ForwarderId",
                table: "Shipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForwarderId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Forwarders_ForwarderId",
                table: "Shipments",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Forwarders_ForwarderId",
                table: "Shipments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForwarderId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Forwarders_ForwarderId",
                table: "Shipments",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
