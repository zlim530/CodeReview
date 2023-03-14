using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDEFCoreOfRicherModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAreaColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area_Value",
                table: "T_Regions",
                newName: "AreaSize");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreaSize",
                table: "T_Regions",
                newName: "Area_Value");
        }
    }
}
