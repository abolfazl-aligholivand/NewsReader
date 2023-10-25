using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsReader.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKeywordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsKeywords_Keywords_FKKeywordId",
                table: "NewsKeywords");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsKeywords_Newses_FKNewsId",
                table: "NewsKeywords");

            migrationBuilder.DropIndex(
                name: "IX_NewsKeywords_FKKeywordId",
                table: "NewsKeywords");

            migrationBuilder.DropIndex(
                name: "IX_NewsKeywords_FKNewsId",
                table: "NewsKeywords");

            migrationBuilder.DropColumn(
                name: "FKKeywordId",
                table: "NewsKeywords");

            migrationBuilder.DropColumn(
                name: "FKNewsId",
                table: "NewsKeywords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKKeywordId",
                table: "NewsKeywords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "FKNewsId",
                table: "NewsKeywords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_NewsKeywords_FKKeywordId",
                table: "NewsKeywords",
                column: "FKKeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsKeywords_FKNewsId",
                table: "NewsKeywords",
                column: "FKNewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsKeywords_Keywords_FKKeywordId",
                table: "NewsKeywords",
                column: "FKKeywordId",
                principalTable: "Keywords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsKeywords_Newses_FKNewsId",
                table: "NewsKeywords",
                column: "FKNewsId",
                principalTable: "Newses",
                principalColumn: "Id");
        }
    }
}
