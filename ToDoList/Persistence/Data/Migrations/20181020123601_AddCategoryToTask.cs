using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Persistence.Data.Migrations
{
    public partial class AddCategoryToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ActiveTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "ActiveTasks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "ActiveTasks",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Categories_CategoryId",
                table: "ActiveTasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CategoryId",
                table: "ActiveTasks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ActiveTasks");
        }
    }
}
