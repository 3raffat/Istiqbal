using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Istiqbal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removesomecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOccupancy",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxOccupancy",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<short>(
                name: "NumberOfGuests",
                table: "Reservations",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
