using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksMgmt.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Age", "DateOfBirth", "Gender", "Name" },
                values: new object[] { 500, 25, new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Female", "XYZ Author" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 500);
        }
    }
}
