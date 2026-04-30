using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PropertyDetails_PropertyId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Properties_PropertyId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnerships_Properties_PropertyId",
                table: "PropertyOwnerships");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "PropertyOwnerships",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwnerships_PropertyId_EndDate",
                table: "PropertyOwnerships",
                newName: "IX_PropertyOwnerships_UnitId_EndDate");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "PropertyDetails",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_PropertyId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_UnitId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Deals",
                newName: "UnitRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_PropertyId",
                table: "Deals",
                newName: "IX_Deals_UnitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PropertyDetails_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Properties_UnitId",
                table: "PropertyDetails",
                column: "UnitId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnerships_Properties_UnitId",
                table: "PropertyOwnerships",
                column: "UnitId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PropertyDetails_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Properties_UnitId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnerships_Properties_UnitId",
                table: "PropertyOwnerships");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "PropertyOwnerships",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwnerships_UnitId_EndDate",
                table: "PropertyOwnerships",
                newName: "IX_PropertyOwnerships_PropertyId_EndDate");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "PropertyDetails",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_UnitId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_PropertyId");

            migrationBuilder.RenameColumn(
                name: "UnitRequestId",
                table: "Deals",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_UnitRequestId",
                table: "Deals",
                newName: "IX_Deals_PropertyId");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Deals_DealId",
                        column: x => x.DealId,
                        principalTable: "Deals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DealId",
                table: "Payments",
                column: "DealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PropertyDetails_PropertyId",
                table: "Deals",
                column: "PropertyId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Properties_PropertyId",
                table: "PropertyDetails",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnerships_Properties_PropertyId",
                table: "PropertyOwnerships",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
