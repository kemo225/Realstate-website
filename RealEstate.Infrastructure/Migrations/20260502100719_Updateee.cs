using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperGalleries_Developers_DeveloperId",
                table: "DeveloperGalleries");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperGalleries_Developers_DeveloperId",
                table: "DeveloperGalleries",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperGalleries_Developers_DeveloperId",
                table: "DeveloperGalleries");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperGalleries_Developers_DeveloperId",
                table: "DeveloperGalleries",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
