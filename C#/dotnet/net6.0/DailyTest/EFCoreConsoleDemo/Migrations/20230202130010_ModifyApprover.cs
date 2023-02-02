using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreConsoleDemo.Migrations
{
    /// <inheritdoc />
    public partial class ModifyApprover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Leaves_T_Users_ApproverId",
                table: "T_Leaves");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverId",
                table: "T_Leaves",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leaves_T_Users_ApproverId",
                table: "T_Leaves",
                column: "ApproverId",
                principalTable: "T_Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Leaves_T_Users_ApproverId",
                table: "T_Leaves");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverId",
                table: "T_Leaves",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Leaves_T_Users_ApproverId",
                table: "T_Leaves",
                column: "ApproverId",
                principalTable: "T_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
