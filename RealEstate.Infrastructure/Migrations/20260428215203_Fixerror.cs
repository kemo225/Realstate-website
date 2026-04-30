using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixerror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_BuyerId",
                table: "Deals");

            migrationBuilder.DropTable(
                name: "UnitOwnerships");

            migrationBuilder.DropIndex(
                name: "IX_Deals_BuyerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnitServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnitImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UnitDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "AgreeCommissionPercentage",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitServices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Units",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitImages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "RefreshTokens",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ProjectImages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Locations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Leads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Leads",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Facilities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Deals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId1",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitServices_CreatedById",
                table: "UnitServices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CreatedById",
                table: "Units",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_CreatedById",
                table: "UnitImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDetails_CreatedById",
                table: "UnitDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedById",
                table: "Services",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_CreatedById",
                table: "RefreshTokens",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_CreatedById",
                table: "ProjectImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_CreatedById",
                table: "Owners",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CreatedById",
                table: "Locations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CreatedById",
                table: "Leads",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_CreatedById",
                table: "Facilities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CreatedById",
                table: "Deals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_OwnerId1",
                table: "Deals",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_AspNetUsers_CreatedById",
                table: "Deals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_OwnerId1",
                table: "Deals",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_AspNetUsers_CreatedById",
                table: "Facilities",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_AspNetUsers_CreatedById",
                table: "Leads",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_CreatedById",
                table: "Locations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_CreatedById",
                table: "Owners",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_AspNetUsers_CreatedById",
                table: "ProjectImages",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_CreatedById",
                table: "RefreshTokens",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_CreatedById",
                table: "Services",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDetails_AspNetUsers_CreatedById",
                table: "UnitDetails",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitImages_AspNetUsers_CreatedById",
                table: "UnitImages",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_AspNetUsers_CreatedById",
                table: "Units",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitServices_AspNetUsers_CreatedById",
                table: "UnitServices",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_AspNetUsers_CreatedById",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_OwnerId1",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_AspNetUsers_CreatedById",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_AspNetUsers_CreatedById",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_CreatedById",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_CreatedById",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_AspNetUsers_CreatedById",
                table: "ProjectImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_CreatedById",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_CreatedById",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitDetails_AspNetUsers_CreatedById",
                table: "UnitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitImages_AspNetUsers_CreatedById",
                table: "UnitImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_AspNetUsers_CreatedById",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitServices_AspNetUsers_CreatedById",
                table: "UnitServices");

            migrationBuilder.DropIndex(
                name: "IX_UnitServices_CreatedById",
                table: "UnitServices");

            migrationBuilder.DropIndex(
                name: "IX_Units_CreatedById",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_UnitImages_CreatedById",
                table: "UnitImages");

            migrationBuilder.DropIndex(
                name: "IX_UnitDetails_CreatedById",
                table: "UnitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Services_CreatedById",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_CreatedById",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectImages_CreatedById",
                table: "ProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_Owners_CreatedById",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CreatedById",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Leads_CreatedById",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_CreatedById",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Deals_CreatedById",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_OwnerId1",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Deals");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnitServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnitImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "UnitDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UnitDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ProjectImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Facilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AgreeCommissionPercentage",
                table: "Deals",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Deals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Deals",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnitOwnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOwnerships_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitOwnerships_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_BuyerId",
                table: "Deals",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOwnerships_OwnerId",
                table: "UnitOwnerships",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOwnerships_UnitId_EndDate",
                table: "UnitOwnerships",
                columns: new[] { "UnitId", "EndDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_BuyerId",
                table: "Deals",
                column: "BuyerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
