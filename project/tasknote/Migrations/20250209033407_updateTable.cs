using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tasknote.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteUser");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NoteId",
                table: "Users",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Notes_NoteId",
                table: "Users",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Notes_NoteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_NoteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "NoteUser",
                columns: table => new
                {
                    NotesNoteId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteUser", x => new { x.NotesNoteId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_NoteUser_Notes_NotesNoteId",
                        column: x => x.NotesNoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUser_UsersUserId",
                table: "NoteUser",
                column: "UsersUserId");
        }
    }
}
