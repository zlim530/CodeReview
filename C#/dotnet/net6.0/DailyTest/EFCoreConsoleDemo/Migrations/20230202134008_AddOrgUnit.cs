using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreConsoleDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddOrgUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_OrgUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OrgUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_OrgUnits_T_OrgUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "T_OrgUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_OrgUnits_ParentId",
                table: "T_OrgUnits",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_OrgUnits");
        }
    }
}
