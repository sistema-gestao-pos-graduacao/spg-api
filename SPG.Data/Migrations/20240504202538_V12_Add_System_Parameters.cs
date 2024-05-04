using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V12_Add_System_Parameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemParams",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Integer = table.Column<int>(type: "int", nullable: true),
                    String = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Boolean = table.Column<bool>(type: "bit", nullable: true),
                    Double = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemParams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemParams");
        }
    }
}
