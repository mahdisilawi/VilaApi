using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class EditDetailModelDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_Vilas_VilaId",
                table: "Detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail",
                table: "Detail");

            migrationBuilder.RenameTable(
                name: "Detail",
                newName: "Details");

            migrationBuilder.RenameIndex(
                name: "IX_Detail_VilaId",
                table: "Details",
                newName: "IX_Details_VilaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_Vilas_VilaId",
                table: "Details",
                column: "VilaId",
                principalTable: "Vilas",
                principalColumn: "VilaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_Vilas_VilaId",
                table: "Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "Detail");

            migrationBuilder.RenameIndex(
                name: "IX_Details_VilaId",
                table: "Detail",
                newName: "IX_Detail_VilaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail",
                table: "Detail",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_Vilas_VilaId",
                table: "Detail",
                column: "VilaId",
                principalTable: "Vilas",
                principalColumn: "VilaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
