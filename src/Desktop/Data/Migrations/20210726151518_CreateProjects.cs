using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracker.Desktop.Data.Migrations
{
    public partial class CreateProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "TrackingEntries",
                newName: "ProjectId");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingEntries_ProjectId",
                table: "TrackingEntries",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingEntries_Projects_ProjectId",
                table: "TrackingEntries",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackingEntries_Projects_ProjectId",
                table: "TrackingEntries");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_TrackingEntries_ProjectId",
                table: "TrackingEntries");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "TrackingEntries",
                newName: "ProjectName");
        }
    }
}
