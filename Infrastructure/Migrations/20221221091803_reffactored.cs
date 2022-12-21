using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class reffactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "JobHistories");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "JobHistories",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndWork",
                table: "JobHistories",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartWork",
                table: "JobHistories",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWork",
                table: "JobHistories");

            migrationBuilder.DropColumn(
                name: "StartWork",
                table: "JobHistories");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "JobHistories",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "JobHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
