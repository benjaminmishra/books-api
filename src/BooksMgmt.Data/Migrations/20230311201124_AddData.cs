using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksMgmt.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Gender", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "J.R.R. Tolkien" },
                    { 2, new DateTime(1892, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Male", "J.R.R. Martin" },
                    { 5, new DateTime(1989, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "JK Rowling" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Genre", "Isbn", "Title" },
                values: new object[,]
                {
                    { 1, 5, "A book about the adventures of Harry Potter", "Adventure", "12AVC456", "Harry Potter" },
                    { 2, 1, "A book about the adventures of Harry Potter", "Fantasy", "89AAVC456", "The Lord Of The Rings" },
                    { 3, 2, "A book about Westrose and its rulers", "Fantasy", "ABC45690", "Song of Ice and Fire" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "xyz@email.com", "XYZ", "password1", 1 },
                    { 2, "efg@email.com", "EFG", "password2", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
