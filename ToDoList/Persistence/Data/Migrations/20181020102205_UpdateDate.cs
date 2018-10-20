using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Persistence.Data.Migrations
{
    public partial class UpdateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "ActiveTasks");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "ActiveTasks",
                newName: "DueDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "ActiveTasks",
                newName: "Start");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "ActiveTasks",
                nullable: true);
        }
    }
}
