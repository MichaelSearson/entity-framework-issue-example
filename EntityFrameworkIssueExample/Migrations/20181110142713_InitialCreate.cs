using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkIssueExample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(nullable: false),
                    AddedByAccountId = table.Column<Guid>(nullable: true),
                    AddedOnUtc = table.Column<DateTime>(nullable: false),
                    ModifiedByAccountId = table.Column<Guid>(nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_AddedByAccountId",
                        column: x => x.AddedByAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ModifiedByAccountId",
                        column: x => x.ModifiedByAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AddedByAccountId",
                table: "Accounts",
                column: "AddedByAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ModifiedByAccountId",
                table: "Accounts",
                column: "ModifiedByAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
