using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V17_Add_Classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Curriculums_CurriculumId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Students",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "CurriculumId",
                table: "Classes",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_CurriculumId",
                table: "Classes",
                newName: "IX_Classes_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "Building",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Classes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Room",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Students",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Building",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Students",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Classes",
                newName: "CurriculumId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_SubjectId",
                table: "Classes",
                newName: "IX_Classes_CurriculumId");

            migrationBuilder.AddColumn<int>(
                name: "Building",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Subjects",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Room",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Students",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Curriculums_CurriculumId",
                table: "Classes",
                column: "CurriculumId",
                principalTable: "Curriculums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
