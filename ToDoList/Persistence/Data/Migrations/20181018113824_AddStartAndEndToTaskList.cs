using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Persistence.Data.Migrations
{
    public partial class AddStartAndEndToTaskList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Notes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Notes");
        }
    }
}
