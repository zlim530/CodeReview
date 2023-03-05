using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySeverDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddT_WordItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_WordItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phonetic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Translation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_WordItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_WordItems");
        }
    }
}
