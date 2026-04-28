using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addservceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_PropertyDetails_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Properties_PropertyId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Projects_ProjectId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Properties_UnitId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnerships_Owners_OwnerId",
                table: "PropertyOwnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyOwnerships_Properties_UnitId",
                table: "PropertyOwnerships");

            migrationBuilder.DropTable(
                name: "PropertyFacilities");

            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyOwnerships",
                table: "PropertyOwnerships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyDetails",
                table: "PropertyDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.RenameTable(
                name: "PropertyOwnerships",
                newName: "UnitOwnerships");

            migrationBuilder.RenameTable(
                name: "PropertyDetails",
                newName: "UnitDetails");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Units");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwnerships_UnitId_EndDate",
                table: "UnitOwnerships",
                newName: "IX_UnitOwnerships_UnitId_EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwnerships_OwnerId",
                table: "UnitOwnerships",
                newName: "IX_UnitOwnerships_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_UnitId",
                table: "UnitDetails",
                newName: "IX_UnitDetails_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ProjectId",
                table: "Units",
                newName: "IX_Units_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnerId",
                table: "Units",
                newName: "IX_Units_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitOwnerships",
                table: "UnitOwnerships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitDetails",
                table: "UnitDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitFacilities",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitFacilities", x => new { x.UnitId, x.FacilityId });
                    table.ForeignKey(
                        name: "FK_UnitFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitFacilities_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitImages_Units_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitServices_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitFacilities_FacilityId",
                table: "UnitFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitImages_PropertyId",
                table: "UnitImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitServices_ServiceId",
                table: "UnitServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitServices_UnitId",
                table: "UnitServices",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_UnitDetails_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "UnitDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Units_PropertyId",
                table: "Leads",
                column: "PropertyId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitDetails_Units_UnitId",
                table: "UnitDetails",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOwnerships_Owners_OwnerId",
                table: "UnitOwnerships",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOwnerships_Units_UnitId",
                table: "UnitOwnerships",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Projects_ProjectId",
                table: "Units",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_UnitDetails_UnitRequestId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Units_PropertyId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitDetails_Units_UnitId",
                table: "UnitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitOwnerships_Owners_OwnerId",
                table: "UnitOwnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitOwnerships_Units_UnitId",
                table: "UnitOwnerships");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Owners_OwnerId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Projects_ProjectId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "UnitFacilities");

            migrationBuilder.DropTable(
                name: "UnitImages");

            migrationBuilder.DropTable(
                name: "UnitServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitOwnerships",
                table: "UnitOwnerships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitDetails",
                table: "UnitDetails");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "UnitOwnerships",
                newName: "PropertyOwnerships");

            migrationBuilder.RenameTable(
                name: "UnitDetails",
                newName: "PropertyDetails");

            migrationBuilder.RenameIndex(
                name: "IX_Units_ProjectId",
                table: "Properties",
                newName: "IX_Properties_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_OwnerId",
                table: "Properties",
                newName: "IX_Properties_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitOwnerships_UnitId_EndDate",
                table: "PropertyOwnerships",
                newName: "IX_PropertyOwnerships_UnitId_EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_UnitOwnerships_OwnerId",
                table: "PropertyOwnerships",
                newName: "IX_PropertyOwnerships_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_UnitDetails_UnitId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyOwnerships",
                table: "PropertyOwnerships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyDetails",
                table: "PropertyDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PropertyFacilities",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    FacilityId = table.Column<int>(type: "int", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFacilities", x => new { x.PropertyId, x.FacilityId });
                    table.ForeignKey(
                        name: "FK_PropertyFacilities_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFacilities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFacilities_FacilityId",
                table: "PropertyFacilities",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_PropertyDetails_UnitRequestId",
                table: "Deals",
                column: "UnitRequestId",
                principalTable: "PropertyDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Properties_PropertyId",
                table: "Leads",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Projects_ProjectId",
                table: "Properties",
                column: "ProjectId",
                principalTable: "Projects",
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
                name: "FK_PropertyOwnerships_Owners_OwnerId",
                table: "PropertyOwnerships",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyOwnerships_Properties_UnitId",
                table: "PropertyOwnerships",
                column: "UnitId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
