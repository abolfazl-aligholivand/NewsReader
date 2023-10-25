using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsReader.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FeedLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FKCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Websites_WebsiteCategories_FKCategoryId",
                        column: x => x.FKCategoryId,
                        principalTable: "WebsiteCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", unicode: false, fixedLength: true, nullable: false),
                    Media = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FKCategoryId = table.Column<int>(type: "int", nullable: false),
                    FKWebsiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Newses_NewsCategories_FKCategoryId",
                        column: x => x.FKCategoryId,
                        principalTable: "NewsCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Newses_Websites_FKWebsiteId",
                        column: x => x.FKWebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NewsKeywords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FKKeywordId = table.Column<int>(type: "int", nullable: false),
                    FKNewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsKeywords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsKeywords_Keywords_FKKeywordId",
                        column: x => x.FKKeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NewsKeywords_Newses_FKNewsId",
                        column: x => x.FKNewsId,
                        principalTable: "Newses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newses_FKCategoryId",
                table: "Newses",
                column: "FKCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Newses_FKWebsiteId",
                table: "Newses",
                column: "FKWebsiteId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsKeywords_FKKeywordId",
                table: "NewsKeywords",
                column: "FKKeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsKeywords_FKNewsId",
                table: "NewsKeywords",
                column: "FKNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Websites_FKCategoryId",
                table: "Websites",
                column: "FKCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsKeywords");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropTable(
                name: "NewsCategories");

            migrationBuilder.DropTable(
                name: "Websites");

            migrationBuilder.DropTable(
                name: "WebsiteCategories");
        }
    }
}
