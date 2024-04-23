using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V5_Subject_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectModelId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                column: "TeacherId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Subjects_SubjectModelId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SubjectModelId",
                table: "Persons");

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
                name: "TeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SubjectModelId",
                table: "Persons");
        }
    }
}
