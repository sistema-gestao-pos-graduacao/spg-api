using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V11_Update_ClassSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ClassSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "ClassSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ClassSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClassSchedules_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Persons_SubjectId",
                table: "ClassSchedules",
                column: "SubjectId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Persons_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ClassSchedules_SubjectId",
                table: "ClassSchedules");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ClassSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "ClassSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ClassSchedules",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSchedules_Persons_TeacherId",
                table: "ClassSchedules",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
