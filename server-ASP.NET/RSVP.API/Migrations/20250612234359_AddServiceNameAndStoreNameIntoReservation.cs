using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSVP.API.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceNameAndStoreNameIntoReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Reservations",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreName",
                table: "Reservations",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StoreName",
                table: "Reservations");
        }
    }
}
