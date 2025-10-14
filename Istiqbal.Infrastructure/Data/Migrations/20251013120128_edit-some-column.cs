using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Istiqbal.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class editsomecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Amenities");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "RoomTypes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "Rooms",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "Reservations",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "RefreshTokens",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "Guests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "Feedbacks",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedUtc",
                table: "Amenities",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "DeletedUtc",
                table: "Amenities");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RoomTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Guests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Feedbacks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Amenities",
                type: "datetime2",
                nullable: true);
        }
    }
}
