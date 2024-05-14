using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPG.Data.Migrations
{
    /// <inheritdoc />
    public partial class V19_Update_Subjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Persons_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
