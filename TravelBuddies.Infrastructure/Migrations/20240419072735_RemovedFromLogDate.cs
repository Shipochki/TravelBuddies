using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFromLogDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2544db52-e788-4e15-bc0a-6e3b764865cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e34f97f-f0cd-4fca-9484-2c094f7579db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f12ef5e-689d-47ef-b6df-b51537d02f59");

            migrationBuilder.DropColumn(
                name: "LogDateTime",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "PriceForSeat",
                table: "Posts",
                newName: "PricePerSeat");

            migrationBuilder.AlterColumn<string>(
                name: "PictureLink",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendTime",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "SendTime",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "PricePerSeat",
                table: "Posts",
                newName: "PriceForSeat");

            migrationBuilder.AlterColumn<string>(
                name: "PictureLink",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "LogDateTime",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2544db52-e788-4e15-bc0a-6e3b764865cd", null, "Admin", null },
                    { "6e34f97f-f0cd-4fca-9484-2c094f7579db", null, "Driver", null },
                    { "8f12ef5e-689d-47ef-b6df-b51537d02f59", null, "Client", null }
                });
        }
    }
}
