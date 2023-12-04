using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEventTableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColor",
                table: "Events",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Events",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColor",
                table: "Events",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
