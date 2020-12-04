using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArkhenManufacturing.DataAccess.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAdmin");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserName",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Admin_UserName",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Admin");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Admin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Admin_LocationId",
                table: "Admin",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Location_LocationId",
                table: "Admin",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Location_LocationId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_LocationId",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LocationAdmin",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAdmin", x => new { x.LocationId, x.AdminId });
                    table.ForeignKey(
                        name: "FK_LocationAdmin_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationAdmin_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserName",
                table: "Customer",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserName",
                table: "Admin",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationAdmin_AdminId",
                table: "LocationAdmin",
                column: "AdminId");
        }
    }
}
