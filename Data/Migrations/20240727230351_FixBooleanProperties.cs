using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModerationCrudApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBooleanProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GoalReduceFrequency",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GoalReduceAmount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GoalReduceFrequency",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "GoalReduceAmount",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: false);
        }
    }

}
