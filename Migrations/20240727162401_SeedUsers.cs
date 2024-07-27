using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModerationCrudApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users (FirstName, LastName, Email, DateOfBirth) VALUES ('John', 'Doe', 'john.doe@example.com', '1980-01-01')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Email = 'john.doe@example.com'");
        }
    }
}
