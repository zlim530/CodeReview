using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDEFCoreOfRicherModel.Migrations
{
    /// <inheritdoc />
    public partial class AddRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Chinese = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_English = table.Column<string>(type: "varchar(255)", nullable: true),
                    Area_Value = table.Column<double>(type: "float", nullable: false),
                    Area_Unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: true),
                    Location_Longitude = table.Column<double>(type: "float", nullable: false),
                    Location_Latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Regions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Regions");
        }
    }
}
