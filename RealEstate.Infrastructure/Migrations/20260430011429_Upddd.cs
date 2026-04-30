using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Upddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeveloperName",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitServices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Units",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitImages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "RefreshTokens",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "ProjectImages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Locations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Leads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Facilities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Deals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Developers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Developers_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperGalleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperGalleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeveloperGalleries_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeveloperGalleries_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeveloperGalleries_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitServices_UpdatedById",
                table: "UnitServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UpdatedById",
                table: "Units",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_UpdatedById",
                table: "UnitImages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitDetails_UpdatedById",
                table: "UnitDetails",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UpdatedById",
                table: "Services",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UpdatedById",
                table: "RefreshTokens",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DeveloperId",
                table: "Projects",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UpdatedById",
                table: "Projects",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_UpdatedById",
                table: "ProjectImages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UpdatedById",
                table: "Owners",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_UpdatedById",
                table: "Locations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_UpdatedById",
                table: "Leads",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_UpdatedById",
                table: "Facilities",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_UpdatedById",
                table: "Deals",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperGalleries_CreatedById",
                table: "DeveloperGalleries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperGalleries_DeveloperId",
                table: "DeveloperGalleries",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperGalleries_UpdatedById",
                table: "DeveloperGalleries",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_CreatedById",
                table: "Developers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_UpdatedById",
                table: "Developers",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_AspNetUsers_UpdatedById",
                table: "Deals",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_AspNetUsers_UpdatedById",
                table: "Facilities",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_AspNetUsers_UpdatedById",
                table: "Leads",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_UpdatedById",
                table: "Locations",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_UpdatedById",
                table: "Owners",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_AspNetUsers_UpdatedById",
                table: "ProjectImages",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UpdatedById",
                table: "Projects",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Developers_DeveloperId",
                table: "Projects",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UpdatedById",
                table: "RefreshTokens",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UpdatedById",
                table: "Services",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDetails_AspNetUsers_UpdatedById",
                table: "UnitDetails",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitImages_AspNetUsers_UpdatedById",
                table: "UnitImages",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_AspNetUsers_UpdatedById",
                table: "Units",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitServices_AspNetUsers_UpdatedById",
                table: "UnitServices",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_AspNetUsers_UpdatedById",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_AspNetUsers_UpdatedById",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_AspNetUsers_UpdatedById",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_UpdatedById",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_UpdatedById",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_AspNetUsers_UpdatedById",
                table: "ProjectImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UpdatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Developers_DeveloperId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UpdatedById",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UpdatedById",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitDetails_AspNetUsers_UpdatedById",
                table: "UnitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitImages_AspNetUsers_UpdatedById",
                table: "UnitImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_AspNetUsers_UpdatedById",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitServices_AspNetUsers_UpdatedById",
                table: "UnitServices");

            migrationBuilder.DropTable(
                name: "DeveloperGalleries");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_UnitServices_UpdatedById",
                table: "UnitServices");

            migrationBuilder.DropIndex(
                name: "IX_Units_UpdatedById",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_UnitImages_UpdatedById",
                table: "UnitImages");

            migrationBuilder.DropIndex(
                name: "IX_UnitDetails_UpdatedById",
                table: "UnitDetails");

            migrationBuilder.DropIndex(
                name: "IX_Services_UpdatedById",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_UpdatedById",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DeveloperId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UpdatedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectImages_UpdatedById",
                table: "ProjectImages");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UpdatedById",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Locations_UpdatedById",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Leads_UpdatedById",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_UpdatedById",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Deals_UpdatedById",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitServices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "UnitDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeveloperName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "ProjectImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
