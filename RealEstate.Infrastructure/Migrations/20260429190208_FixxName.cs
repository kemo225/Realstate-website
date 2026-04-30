using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixxName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Units_PropertyId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_PropertyId",
                table: "Leads");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Leads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_UnitId",
                table: "Leads",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_UnitId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Leads");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_PropertyId",
                table: "Leads",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Units_PropertyId",
                table: "Leads",
                column: "PropertyId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
