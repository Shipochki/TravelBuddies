using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e8ac22b-fe9e-4179-b4df-687e3619bd42", null, "client", "CLIENT" },
                    { "57198ec5-bf97-427d-9a79-982db8c08049", null, "admin", "ADMIN" },
                    { "e3561acf-d05f-494a-95b0-7e12433ec036", null, "driver", "DRIVER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "CreatedOn", "DeletedOn", "DriverLicenseBackPictureLink", "DriverLicenseFrontPictureLink", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureLink", "SecurityStamp", "TwoFactorEnabled", "UpdatedOn", "UserName" },
                values: new object[,]
                {
                    { "050609c6-76d0-4a15-b9d8-bb70da19d1fe", 0, null, "2f8f0326-49df-4896-bff4-bd06fae02f72", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "stef@abv.bg", false, "Stefan", false, "Petkov", false, null, "STEF@ABV.BG", "STEF@ABV.BG", "AQAAAAIAAYagAAAAEPVnteRPP3bxhfn6aX/VLDyvnhpR1G/XMZL+3lTqsFfCBSkw02SQmqBTA3eYyp8iGw==", null, false, null, "KSGYS5WQKJHNDDGNY4GS4EX76VXJQ7OI", false, null, "stef@abv.bg" },
                    { "b33a74a9-0021-4c3a-9481-a6fc6a922655", 0, "Pernik", "fc339f9a-b858-4016-8b88-ed0a66d016f2", "Bulgaria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "ivan@mail.com", false, "Ivan", false, "Marianov", false, null, "IVAN@MAIL.COM", "IVAN@MAIL.COM", "AQAAAAIAAYagAAAAEAp54NNJZSWCgUnjr3egFsEaeGZn+LaS5ln6Vy5fdWhxgSRunRtN7ZwkbZTzp0m2xw==", null, false, null, "6Y6MZSEJ5FKGOXWADDON74PWO7EYV7MJ", false, null, "ivan@mail.com" },
                    { "fd325402-6f4e-4657-8b05-bc141b17cdef", 0, null, "f331cadb-1aa2-40a2-9a4f-caf2d56ea45c", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "admin@gmail.com", false, "Admin", false, "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMh/VZgfL1JHlQRYRVq9jjyXpX4SwQYPxfO6MgLj0fjy5MsBOxg6Orr5MS9h8NV5ww==", null, false, null, "D2VRP2BMPH4QOFVUC3LMIHZVDKCALXNZ", false, null, "admin@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0e8ac22b-fe9e-4179-b4df-687e3619bd42", "050609c6-76d0-4a15-b9d8-bb70da19d1fe" },
                    { "e3561acf-d05f-494a-95b0-7e12433ec036", "b33a74a9-0021-4c3a-9481-a6fc6a922655" },
                    { "57198ec5-bf97-427d-9a79-982db8c08049", "fd325402-6f4e-4657-8b05-bc141b17cdef" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0e8ac22b-fe9e-4179-b4df-687e3619bd42", "050609c6-76d0-4a15-b9d8-bb70da19d1fe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e3561acf-d05f-494a-95b0-7e12433ec036", "b33a74a9-0021-4c3a-9481-a6fc6a922655" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "57198ec5-bf97-427d-9a79-982db8c08049", "fd325402-6f4e-4657-8b05-bc141b17cdef" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e8ac22b-fe9e-4179-b4df-687e3619bd42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57198ec5-bf97-427d-9a79-982db8c08049");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3561acf-d05f-494a-95b0-7e12433ec036");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "050609c6-76d0-4a15-b9d8-bb70da19d1fe");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33a74a9-0021-4c3a-9481-a6fc6a922655");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd325402-6f4e-4657-8b05-bc141b17cdef");
        }
    }
}
