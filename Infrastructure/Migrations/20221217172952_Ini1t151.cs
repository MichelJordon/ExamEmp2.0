using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ini1t151 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories");

            migrationBuilder.AddColumn<int>(
                name: "JobHistoryId",
                table: "JobHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories",
                column: "JobHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories");

            migrationBuilder.DropColumn(
                name: "JobHistoryId",
                table: "JobHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories",
                column: "StartDate");
        }
    }
}
