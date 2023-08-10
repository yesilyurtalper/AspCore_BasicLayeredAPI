using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasicLayeredService.API.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "varchar(10000)", maxLength: 10000, nullable: false),
                    Author = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "DateCreated", "DateModified", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "alpery", new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(832), new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(840), "Some user post by alpery", "postA" },
                    { 2, "alpery", new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(841), new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(841), "Some user post by alpery", "postB" },
                    { 3, "alpery", new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(842), new DateTime(2023, 8, 9, 0, 16, 4, 301, DateTimeKind.Local).AddTicks(843), "Some user post by alpery", "postC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Author",
                table: "Posts",
                column: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
