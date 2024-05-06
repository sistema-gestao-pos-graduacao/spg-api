using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V15_Update_ClassSchedule_Remove_TeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Persons_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Subjects_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Subjects_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Persons_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
