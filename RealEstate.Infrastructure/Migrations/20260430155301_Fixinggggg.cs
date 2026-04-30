using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixinggggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentYears",
                table: "PaymentPlans");

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentMothes",
                table: "PaymentPlans",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Locations",
                type: "nvarchar(max)",
                precision: 11,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)",
                oldPrecision: 11,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Locations",
                type: "nvarchar(max)",
                precision: 10,
                scale: 8,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)",
                oldPrecision: 10,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealType",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentMothes",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "DealType",
                table: "Deals");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentYears",
                table: "PaymentPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Locations",
                type: "decimal(11,8)",
                precision: 11,
                scale: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldPrecision: 11,
                oldScale: 8,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Locations",
                type: "decimal(10,8)",
                precision: 10,
                scale: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldPrecision: 10,
                oldScale: 8,
                oldNullable: true);
        }
    }
}
