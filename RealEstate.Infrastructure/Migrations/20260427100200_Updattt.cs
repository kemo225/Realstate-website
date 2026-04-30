using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updattt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_SellerId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Properties_PropertyId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_PropertyId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_SellerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "InstallmentDownPayment",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "InstallmentYears",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "DealType",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Properties",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Area",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SoldCount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PropertyDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstallmentYears = table.Column<int>(type: "int", nullable: true),
                    InstallmentDownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ApprovedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_OwnerId",
                table: "Deals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_PropertyId",
                table: "Deals",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_PropertyId",
                table: "PropertyDetails",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_OwnerId",
                table: "Deals",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PropertyDetails_PropertyId",
                table: "Deals",
                column: "PropertyId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_OwnerId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PropertyDetails_PropertyId",
                table: "Deals");

            migrationBuilder.DropTable(
                name: "PropertyDetails");

            migrationBuilder.DropIndex(
                name: "IX_Deals_OwnerId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_PropertyId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "SoldCount",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Properties",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Properties",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InstallmentDownPayment",
                table: "Properties",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentYears",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DealType",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_PropertyId",
                table: "Deals",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_SellerId",
                table: "Deals",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_SellerId",
                table: "Deals",
                column: "SellerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Properties_PropertyId",
                table: "Deals",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
