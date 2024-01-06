using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountTransactions.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "CategoryCondition",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "CategoryCondition");
        }
    }
}
