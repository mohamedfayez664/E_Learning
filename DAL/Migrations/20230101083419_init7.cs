using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseReview");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserCourse",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewText",
                table: "UserCourse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SEvaluation",
                table: "UserCourse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TEvaluation",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "ReviewText",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "SEvaluation",
                table: "UserCourse");

            migrationBuilder.DropColumn(
                name: "TEvaluation",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SEvaluation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TEvaluation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseReview_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseReview_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_CourseId",
                table: "CourseReview",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReview_UserId",
                table: "CourseReview",
                column: "UserId");
        }
    }
}
