using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V8_Add_Subject_Syllabus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Syllabus",
                table: "Subjects",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Syllabus",
                table: "Subjects");
        }
    }
}
