using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V14_Update_ClassSchedule_Remove_TeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "ClassSchedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "ClassSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
