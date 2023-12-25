using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountTransactions.Api.Migrations
{
    /// <inheritdoc />
    public partial class addImportFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImportFileId",
                table: "Transactions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ImportFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Filename = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFiles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ImportFileId",
                table: "Transactions",
                column: "ImportFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ImportFiles_ImportFileId",
                table: "Transactions",
                column: "ImportFileId",
                principalTable: "ImportFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ImportFiles_ImportFileId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "ImportFiles");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ImportFileId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ImportFileId",
                table: "Transactions");
        }
    }
}
