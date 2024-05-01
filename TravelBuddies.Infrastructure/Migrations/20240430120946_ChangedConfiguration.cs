using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Posts_PostId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Groups_PostId",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Groups_Id",
                table: "Posts",
                column: "Id",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Groups_GroupId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Groups_Id",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Messages_GroupId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PostId",
                table: "Groups",
                column: "PostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Posts_PostId",
                table: "Groups",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
