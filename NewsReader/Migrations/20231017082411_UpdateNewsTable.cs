using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsReader.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewsGuid",
                table: "Newses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "NewsGuid",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Newses");
        }
    }
}
