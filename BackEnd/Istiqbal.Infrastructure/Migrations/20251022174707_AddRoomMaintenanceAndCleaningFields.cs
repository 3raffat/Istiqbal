using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Istiqbal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomMaintenanceAndCleaningFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CleaningStartTime",
                table: "Rooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "Rooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MaintenanceStartTime",
                table: "Rooms",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CleaningStartTime",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaintenanceStartTime",
                table: "Rooms");
        }
    }
}
