using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMessageSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Groups_GroupId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_GroupId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GroupId1",
                table: "Messages",
                column: "GroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Groups_GroupId1",
                table: "Messages",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
