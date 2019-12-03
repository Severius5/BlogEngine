using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BlogEngine.Migrations.PostgreMigrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 100, nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    NormalizedUsername = table.Column<string>(maxLength: 100, nullable: false),
                    Slug = table.Column<string>(maxLength: 100, nullable: false),
                    Bio = table.Column<string>(maxLength: 1024, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DetailsStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    NormalizedTitle = table.Column<string>(maxLength: 150, nullable: false),
                    Slug = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 450, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PublicationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Bio", "CreationDate", "DetailsStamp", "Email", "IsAdmin", "NormalizedEmail", "NormalizedUsername", "Password", "Slug", "Username" },
                values: new object[] { 1, "Write here some information about yourself.  You can use markdown here.", new DateTime(2018, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "DA56E937-FF2C-4401-9DC2-E353DD474346", "admin@admin.admin", true, "ADMIN@ADMIN.ADMIN", "ADMINISTRATOR", "AQAAAAEAACcaAAAAECekU4gLOtHZ4lSHACsEr1UjxJ5fo2dUjXTM1Rq8ZeRZzkcq+h8zWlLgtJ2LGpUU1w==", "administrator", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_posts_AuthorId",
                table: "posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_NormalizedTitle",
                table: "posts",
                column: "NormalizedTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_NormalizedEmail",
                table: "users",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_NormalizedUsername",
                table: "users",
                column: "NormalizedUsername",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
