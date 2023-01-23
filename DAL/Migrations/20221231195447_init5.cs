using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupDiscussions");

            migrationBuilder.AddColumn<string>(
                name: "Discussion",
                table: "PlayListGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discussion",
                table: "PlayListGroups");

            migrationBuilder.CreateTable(
                name: "GroupDiscussions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayListId = table.Column<int>(type: "int", nullable: false),
                    StGroupId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDiscussions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDiscussions_PlayLists_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDiscussions_StGroups_StGroupId",
                        column: x => x.StGroupId,
                        principalTable: "StGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDiscussions_PlayListId",
                table: "GroupDiscussions",
                column: "PlayListId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDiscussions_StGroupId",
                table: "GroupDiscussions",
                column: "StGroupId");
        }
    }
}
