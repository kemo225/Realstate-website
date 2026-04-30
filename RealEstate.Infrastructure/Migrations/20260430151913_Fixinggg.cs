using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixinggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Leads");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Leads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Units_UnitId",
                table: "Leads",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
