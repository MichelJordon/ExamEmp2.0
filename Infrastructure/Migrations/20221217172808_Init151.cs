using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init151 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistories_Employees_EmployeeId1",
                table: "JobHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories");

            migrationBuilder.DropIndex(
                name: "IX_JobHistories_EmployeeId1",
                table: "JobHistories");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "JobHistories");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "JobHistories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_EmployeeId",
                table: "JobHistories",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistories_Employees_EmployeeId",
                table: "JobHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHistories_Employees_EmployeeId",
                table: "JobHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories");

            migrationBuilder.DropIndex(
                name: "IX_JobHistories_EmployeeId",
                table: "JobHistories");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "JobHistories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "JobHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHistories_EmployeeId1",
                table: "JobHistories",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHistories_Employees_EmployeeId1",
                table: "JobHistories",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
