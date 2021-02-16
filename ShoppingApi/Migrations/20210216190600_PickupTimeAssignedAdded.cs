using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApi.Migrations
{
    public partial class PickupTimeAssignedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PickupTimeAssigned",
                table: "CurbsideOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupTimeAssigned",
                table: "CurbsideOrders");
        }
    }
}
