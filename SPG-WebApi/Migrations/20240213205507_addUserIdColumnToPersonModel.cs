using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdColumnToPersonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Persons");
        }
    }
}
