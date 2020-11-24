using Microsoft.EntityFrameworkCore.Migrations;

namespace ArkhenManufacturing.DataAccess.Migrations
{
    public partial class LocationHasNameNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Admin_AdminId1",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Location_LocationId1",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "LocationId1",
                table: "Order",
                newName: "DbLocationId");

            migrationBuilder.RenameColumn(
                name: "AdminId1",
                table: "Order",
                newName: "DbAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_LocationId1",
                table: "Order",
                newName: "IX_Order_DbLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AdminId1",
                table: "Order",
                newName: "IX_Order_DbAdminId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Admin_DbAdminId",
                table: "Order",
                column: "DbAdminId",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Location_DbLocationId",
                table: "Order",
                column: "DbLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Admin_DbAdminId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Location_DbLocationId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "DbLocationId",
                table: "Order",
                newName: "LocationId1");

            migrationBuilder.RenameColumn(
                name: "DbAdminId",
                table: "Order",
                newName: "AdminId1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DbLocationId",
                table: "Order",
                newName: "IX_Order_LocationId1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DbAdminId",
                table: "Order",
                newName: "IX_Order_AdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Admin_AdminId1",
                table: "Order",
                column: "AdminId1",
                principalTable: "Admin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Location_LocationId1",
                table: "Order",
                column: "LocationId1",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
