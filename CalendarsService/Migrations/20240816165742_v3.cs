using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarsService.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Calendars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
