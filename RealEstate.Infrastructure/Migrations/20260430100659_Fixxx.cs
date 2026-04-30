using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_OwnerId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_OwnerId1",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_UnitDetails_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "UnitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Units_OwnerId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Deals_OwnerId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_OwnerId1",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Deals");

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applicants_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstallmentYears = table.Column<int>(type: "int", nullable: true),
                    InstallmentDownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlans_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPlans_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPlans_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPlans_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId1 = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Applicants_ApplicantId1",
                        column: x => x.ApplicantId1,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Units_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_CreatedById",
                table: "Applicants",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_UpdatedById",
                table: "Applicants",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_ApprovedById",
                table: "PaymentPlans",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_CreatedById",
                table: "PaymentPlans",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_UnitId",
                table: "PaymentPlans",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlans_UpdatedById",
                table: "PaymentPlans",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicantId",
                table: "Requests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicantId1",
                table: "Requests",
                column: "ApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedById",
                table: "Requests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UpdatedById",
                table: "Requests",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropTable(
                name: "PaymentPlans");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Leads",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId1",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owners_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentDownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstallmentYears = table.Column<int>(type: "int", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitDetails_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitDetails_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_OwnerId",
                table: "Units",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_OwnerId",
                table: "Deals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_OwnerId1",
                table: "Deals",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CreatedById",
                table: "Owners",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UpdatedById",
                table: "Owners",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDetails_CreatedById",
                table: "UnitDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDetails_UnitId",
                table: "UnitDetails",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDetails_UpdatedById",
                table: "UnitDetails",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_OwnerId",
                table: "Deals",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_OwnerId1",
                table: "Deals",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_UnitDetails_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "UnitDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
