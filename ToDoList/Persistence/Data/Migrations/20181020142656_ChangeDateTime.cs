using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Persistence.Data.Migrations
{
    public partial class ChangeDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "ActiveTasks",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "ActiveTasks",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
