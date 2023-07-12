using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class threadUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Epics_EpicId1",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserId1",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Threads",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EpicId1",
                table: "Threads",
                newName: "EpicId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserId1",
                table: "Threads",
                newName: "IX_Threads_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_EpicId1",
                table: "Threads",
                newName: "IX_Threads_EpicId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Epics_EpicId",
                table: "Threads",
                column: "EpicId",
                principalTable: "Epics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Epics_EpicId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_UserId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Threads");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Threads",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "EpicId",
                table: "Threads",
                newName: "EpicId1");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_UserId",
                table: "Threads",
                newName: "IX_Threads_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_EpicId",
                table: "Threads",
                newName: "IX_Threads_EpicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Epics_EpicId1",
                table: "Threads",
                column: "EpicId1",
                principalTable: "Epics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_UserId1",
                table: "Threads",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
