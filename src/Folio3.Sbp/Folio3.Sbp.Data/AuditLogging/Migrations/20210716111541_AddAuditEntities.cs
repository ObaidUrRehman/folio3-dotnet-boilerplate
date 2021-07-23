using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Folio3.DotNet.Sbp.Data.Migrations
{
    public partial class AddAuditEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AuditLogs",
                table => new
                {
                    Id = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    UserEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    EntityState = table.Column<int>("int"),
                    TableName = table.Column<string>("nvarchar(450)"),
                    RecordId = table.Column<string>("nvarchar(256)", maxLength: 256),
                    TimeStamp = table.Column<DateTime>("datetime2")
                },
                constraints: table => { table.PrimaryKey("PK_AuditLogs", x => x.Id); });

            migrationBuilder.CreateTable(
                "AuditLogChanges",
                table => new
                {
                    Id = table.Column<long>("bigint")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditLogId = table.Column<long>("bigint"),
                    Property = table.Column<string>("nvarchar(max)"),
                    Original = table.Column<string>("nvarchar(max)", nullable: true),
                    Current = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogChanges", x => x.Id);
                    table.ForeignKey(
                        "FK_AuditLogChanges_AuditLogs_AuditLogId",
                        x => x.AuditLogId,
                        "AuditLogs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_AuditLogChanges_AuditLogId",
                "AuditLogChanges",
                "AuditLogId");

            migrationBuilder.CreateIndex(
                "IX_AuditLogs_RecordId",
                "AuditLogs",
                "RecordId");

            migrationBuilder.CreateIndex(
                "IX_AuditLogs_TableName",
                "AuditLogs",
                "TableName");

            migrationBuilder.CreateIndex(
                "IX_AuditLogs_TimeStamp",
                "AuditLogs",
                "TimeStamp");

            migrationBuilder.CreateIndex(
                "IX_AuditLogs_UserEmail",
                "AuditLogs",
                "UserEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AuditLogChanges");

            migrationBuilder.DropTable(
                "AuditLogs");
        }
    }
}