using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySeverDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddWeChatAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WeChatAccount",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeChatAccount",
                table: "AspNetUsers");
        }
    }
}
