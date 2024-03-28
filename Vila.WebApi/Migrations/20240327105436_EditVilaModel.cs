using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EditVilaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DayPrice",
                table: "Vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SellPrice",
                table: "Vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayPrice",
                table: "Vilas");

            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "Vilas");
        }
    }
}
