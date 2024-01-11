using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDiary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGroupIdFieldInEventForEventDrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Groups_GroupId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Events",
                newName: "GroupIdentificator");

            migrationBuilder.RenameIndex(
                name: "IX_Events_GroupId",
                table: "Events",
                newName: "IX_Events_GroupIdentificator");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events",
                column: "GroupIdentificator",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "GroupIdentificator",
                table: "Events",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_GroupIdentificator",
                table: "Events",
                newName: "IX_Events_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Groups_GroupId",
                table: "Events",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
