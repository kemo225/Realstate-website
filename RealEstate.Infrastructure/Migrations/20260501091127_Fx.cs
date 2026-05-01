using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fx : Migration
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

            migrationBuilder.CreateTable(
                name: "UnitSoldouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    SoldoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitSoldouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitSoldouts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitSoldouts_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitSoldouts_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitSoldouts_CreatedById",
                table: "UnitSoldouts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitSoldouts_UnitId",
                table: "UnitSoldouts",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitSoldouts_UpdatedById",
                table: "UnitSoldouts",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals",
                column: "UnitPlanId",
                principalTable: "PaymentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PaymentPlans_UnitPlanId",
                table: "Deals");

            migrationBuilder.DropTable(
                name: "UnitSoldouts");

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
    }
}
