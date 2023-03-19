using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksMgmt.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genre", "Isbn", "Title" },
                values: new object[] { 5, 500, "A book about Westrose and its rulers", "Fantasy", "ABC45690", "Song of Ice and Fire2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
