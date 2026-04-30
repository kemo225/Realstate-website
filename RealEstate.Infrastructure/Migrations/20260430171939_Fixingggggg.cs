using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixingggggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "UnitPlanId",
                table: "Deals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LocationDeal",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                unique: true,
                filter: "[UnitPlanId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "LocationDeal",
                table: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "UnitPlanId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
