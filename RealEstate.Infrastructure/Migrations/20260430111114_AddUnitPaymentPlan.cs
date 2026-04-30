using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPaymentPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Applicants_ApplicantId1",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Units_ApplicantId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApplicantId1",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ApplicantId1",
                table: "Requests",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "UnitRequestId",
                table: "Deals",
                newName: "UnitPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_UnitRequestId",
                table: "Deals",
                newName: "IX_Deals_UnitPlanId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnitPaymentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    DownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentYears = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPaymentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitPaymentPlans_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitPaymentPlans_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitPaymentPlans_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApprovedById",
                table: "Requests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UnitId",
                table: "Requests",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPaymentPlans_CreatedById",
                table: "UnitPaymentPlans",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPaymentPlans_UnitId",
                table: "UnitPaymentPlans",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPaymentPlans_UpdatedById",
                table: "UnitPaymentPlans",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Applicants_ApplicantId",
                table: "Requests",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ApprovedById",
                table: "Requests",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Units_UnitId",
                table: "Requests",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Applicants_ApplicantId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ApprovedById",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Units_UnitId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "UnitPaymentPlans");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApprovedById",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_UnitId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Requests",
                newName: "ApplicantId1");

            migrationBuilder.RenameColumn(
                name: "UnitPlanId",
                table: "Deals",
                newName: "UnitRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals",
                newName: "IX_Deals_UnitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicantId1",
                table: "Requests",
                column: "ApplicantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Applicants_ApplicantId1",
                table: "Requests",
                column: "ApplicantId1",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Units_ApplicantId",
                table: "Requests",
                column: "ApplicantId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
