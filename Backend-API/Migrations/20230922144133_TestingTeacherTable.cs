using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_API.Migrations
{
    /// <inheritdoc />
    public partial class TestingTeacherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7407b086-a71d-4450-a455-360247804e89", 0, "d819d338-a3fc-49a1-9e55-241754966da6", "yahya.zf2@gmail.com", false, false, null, null, null, null, "1234567890", false, "a417d4ea-969b-498e-9a35-6576f4200695", false, "yoyo" });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "Avatar", "BirthDate" },
                values: new object[] { "7407b086-a71d-4450-a455-360247804e89", "somethings random", new DateTime(2023, 9, 22, 21, 41, 33, 275, DateTimeKind.Local).AddTicks(8624) });

            migrationBuilder.InsertData(
                table: "Teachers",
                column: "Id",
                value: "7407b086-a71d-4450-a455-360247804e89");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "7407b086-a71d-4450-a455-360247804e89");

            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "7407b086-a71d-4450-a455-360247804e89");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7407b086-a71d-4450-a455-360247804e89");
        }
    }
}
