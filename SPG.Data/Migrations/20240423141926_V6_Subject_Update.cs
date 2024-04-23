using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V6_Subject_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Subjects_SubjectModelId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SubjectModelId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SubjectModelId",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "Students",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "WeekDay",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Students",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "WeekDay",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectModelId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SubjectModelId",
                table: "Persons",
                column: "SubjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Subjects_SubjectModelId",
                table: "Persons",
                column: "SubjectModelId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
