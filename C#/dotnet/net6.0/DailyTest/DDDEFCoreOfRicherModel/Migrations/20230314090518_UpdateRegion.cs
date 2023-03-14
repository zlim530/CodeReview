using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDEFCoreOfRicherModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Area_Value",
                table: "T_Regions",
                type: "float",
                nullable: false,
                comment: "区域大小",
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Area_Unit",
                table: "T_Regions",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Area_Value",
                table: "T_Regions",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "区域大小");

            migrationBuilder.AlterColumn<string>(
                name: "Area_Unit",
                table: "T_Regions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);
        }
    }
}
