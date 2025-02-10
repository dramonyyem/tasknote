using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tasknote.Migrations
{
    /// <inheritdoc />
    public partial class impleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
