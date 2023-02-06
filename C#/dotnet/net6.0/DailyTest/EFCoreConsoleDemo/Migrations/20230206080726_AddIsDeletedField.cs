using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreConsoleDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "T_Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "T_Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "T_Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "T_Articles");
        }
    }
}
