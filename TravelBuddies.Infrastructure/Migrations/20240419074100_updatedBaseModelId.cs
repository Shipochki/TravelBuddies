using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedBaseModelId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bc2505c-76cf-4af3-90c1-21a96837386d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a447303-8638-4fd8-85b0-76993239d37b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd35ad40-c738-49eb-a896-15ea0a718322");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1196f864-03bf-4336-9355-f83ce48a3a03", null, "Admin", null },
                    { "1ededb2c-7787-4683-b93e-5c79ec0ffdab", null, "Client", null },
                    { "9e4281e2-70b7-4f8a-a47a-77c95e0872c4", null, "Driver", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1196f864-03bf-4336-9355-f83ce48a3a03");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ededb2c-7787-4683-b93e-5c79ec0ffdab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e4281e2-70b7-4f8a-a47a-77c95e0872c4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bc2505c-76cf-4af3-90c1-21a96837386d", null, "Client", null },
                    { "8a447303-8638-4fd8-85b0-76993239d37b", null, "Admin", null },
                    { "bd35ad40-c738-49eb-a896-15ea0a718322", null, "Driver", null }
                });
        }
    }
}
