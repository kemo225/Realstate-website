using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixingg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_AspNetUsers_ApprovedById",
                table: "PaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPlans_ApprovedById",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "PaymentPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "PaymentPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "PaymentPlans",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_ApprovedById",
                table: "PaymentPlans",
                column: "ApprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_AspNetUsers_ApprovedById",
                table: "PaymentPlans",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
