using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class timesheet_table_expand_effort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Effort",
                table: "Timesheets",
                newName: "WednesdayEffort");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "Timesheets",
                newName: "StartDay");

            migrationBuilder.AddColumn<int>(
                name: "FridayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MondayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaturdayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SundayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThursdayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TuesdayEffort",
                table: "Timesheets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayEffort",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "MondayEffort",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "SaturdayEffort",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "SundayEffort",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "ThursdayEffort",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "TuesdayEffort",
                table: "Timesheets");

            migrationBuilder.RenameColumn(
                name: "WednesdayEffort",
                table: "Timesheets",
                newName: "Effort");

            migrationBuilder.RenameColumn(
                name: "StartDay",
                table: "Timesheets",
                newName: "Day");
        }
    }
}
