using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class CompanyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "JobApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    RecruiterName = table.Column<string>(type: "TEXT", nullable: false),
                    RecruiterEmail = table.Column<string>(type: "TEXT", nullable: false),
                    RecruiterPhone = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CompanyId",
                table: "JobApplications",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Company_CompanyId",
                table: "JobApplications",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Company_CompanyId",
                table: "JobApplications");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_CompanyId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "JobApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
