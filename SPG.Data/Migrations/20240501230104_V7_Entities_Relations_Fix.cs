using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V7_Entities_Relations_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CoordinatorId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CoordinatorId",
                table: "Courses",
                column: "CoordinatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CoordinatorId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_CourseId",
                table: "Curriculums",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CoordinatorId",
                table: "Courses",
                column: "CoordinatorId",
                unique: true,
                filter: "[CoordinatorId] IS NOT NULL");
        }
    }
}
