using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Habits.Infrastructure.Migrations
{
    public partial class InitHabits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "habits");

            migrationBuilder.CreateTable(
                name: "Habits",
                schema: "habits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Days = table.Column<int>(type: "int", nullable: false),
                    DailyGoal = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyProgress",
                schema: "habits",
                columns: table => new
                {
                    HabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyProgress", x => new { x.Date, x.HabitId });
                    table.ForeignKey(
                        name: "FK_DailyProgress_Habits_HabitId",
                        column: x => x.HabitId,
                        principalSchema: "habits",
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminder",
                schema: "habits",
                columns: table => new
                {
                    HabitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => new { x.Time, x.HabitId });
                    table.ForeignKey(
                        name: "FK_Reminder_Habits_HabitId",
                        column: x => x.HabitId,
                        principalSchema: "habits",
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyProgress_HabitId",
                schema: "habits",
                table: "DailyProgress",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_HabitId",
                schema: "habits",
                table: "Reminder",
                column: "HabitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyProgress",
                schema: "habits");

            migrationBuilder.DropTable(
                name: "Reminder",
                schema: "habits");

            migrationBuilder.DropTable(
                name: "Habits",
                schema: "habits");
        }
    }
}
