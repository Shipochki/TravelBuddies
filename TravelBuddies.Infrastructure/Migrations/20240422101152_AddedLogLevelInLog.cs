using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLogLevelInLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogLevel",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogLevel",
                table: "Logs");
        }
    }
}
