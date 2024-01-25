using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDiary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviorForEventGroupFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events",
                column: "GroupIdentificator",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Groups_GroupIdentificator",
                table: "Events",
                column: "GroupIdentificator",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
