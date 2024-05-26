using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Posts",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "EUR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Posts");
        }
    }
}
