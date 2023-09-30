using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_API.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculty_FacultyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty");

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

            migrationBuilder.RenameTable(
                name: "Faculty",
                newName: "Faculties");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Students",
                newName: "FacultyID");

            migrationBuilder.AlterColumn<string>(
                name: "FacultyID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "DirectionID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroupID",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Directions_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JailedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JailedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false),
                    isOnline = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    Directionid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Directions_Directionid",
                        column: x => x.Directionid,
                        principalTable: "Directions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Literature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineGroup",
                columns: table => new
                {
                    DisciplinesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineGroup", x => new { x.DisciplinesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_DisciplineGroup_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineTeacher",
                columns: table => new
                {
                    DisciplinesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeachersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineTeacher", x => new { x.DisciplinesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_DisciplineTeacher_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplineTeacher_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directions_FacultyId",
                table: "Directions",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineGroup_GroupsId",
                table: "DisciplineGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_ScheduleId",
                table: "Disciplines",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineTeacher_TeachersId",
                table: "DisciplineTeacher",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Directionid",
                table: "Groups",
                column: "Directionid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplineGroup");

            migrationBuilder.DropTable(
                name: "DisciplineTeacher");

            migrationBuilder.DropTable(
                name: "JailedUsers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DirectionID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "Faculty");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Students",
                newName: "FacultyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FacultyId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculty_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
