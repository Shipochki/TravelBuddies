using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameBaseEntityProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8810ee91-5bb9-4b62-8e5e-fbbe72be36f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d79cd296-d255-40d8-b60b-14d728fe33ef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd765384-877f-4ff7-8809-9c17ed23a3b4");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Vehicles",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Reviews",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Posts",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Messages",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Logs",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Groups",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Countries",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Cities",
                newName: "CreatedOn");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Vehicles",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Reviews",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Posts",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Messages",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Logs",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Groups",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Countries",
                newName: "CreateOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Cities",
                newName: "CreateOn");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8810ee91-5bb9-4b62-8e5e-fbbe72be36f4", null, "Client", null },
                    { "d79cd296-d255-40d8-b60b-14d728fe33ef", null, "Admin", null },
                    { "dd765384-877f-4ff7-8809-9c17ed23a3b4", null, "Driver", null }
                });
        }
    }
}
