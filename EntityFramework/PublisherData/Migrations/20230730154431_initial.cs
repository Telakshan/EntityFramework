using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Rhoda", "Lerman" },
<<<<<<<< HEAD:EntityFramework/PublisherData/Migrations/20230802164747_initial.cs
                    { 2, "Sofia", "Segovia" }
========
                    { 2, "Sofia", "Segovia" },
                    { 3, "Hasan", "Piker" },
                    { 4, "Joe", "Abercrombie" },
                    { 5, "Stephen", "King" }
>>>>>>>> da131532fe2d8e8914958fd830dda0349aa1740c:EntityFramework/PublisherData/Migrations/20230730154431_initial.cs
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "BasePrice", "Genre", "PublishDate", "Title" },
<<<<<<<< HEAD:EntityFramework/PublisherData/Migrations/20230802164747_initial.cs
                values: new object[] { 1, 2, 8.0m, "Coding", new DateTime(2023, 8, 2, 12, 47, 47, 243, DateTimeKind.Local).AddTicks(6259), "Entity Framework" });
========
                values: new object[] { 1, 4, 20.0m, "", new DateTime(2023, 7, 30, 11, 44, 31, 740, DateTimeKind.Local).AddTicks(5120), "Before they are hanged" });
>>>>>>>> da131532fe2d8e8914958fd830dda0349aa1740c:EntityFramework/PublisherData/Migrations/20230730154431_initial.cs

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
