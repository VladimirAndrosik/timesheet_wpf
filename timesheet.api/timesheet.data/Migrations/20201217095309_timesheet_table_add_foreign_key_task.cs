using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class timesheet_table_add_foreign_key_task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_TaskId",
                table: "Timesheets",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Tasks_TaskId",
                table: "Timesheets",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Tasks_TaskId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_TaskId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Timesheets");
        }
    }
}
