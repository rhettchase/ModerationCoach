using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModerationCrudApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOptionalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Users",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "DrinksPerWeek",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GoalReduceAmount",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GoalReduceFrequency",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDryDays",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReduceByDays",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReduceByDrinks",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrinksPerWeek",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoalReduceAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoalReduceFrequency",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberOfDryDays",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReduceByDays",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReduceByDrinks",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
