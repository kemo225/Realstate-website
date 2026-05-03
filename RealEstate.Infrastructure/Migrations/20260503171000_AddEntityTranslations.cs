using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityTranslations_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityTranslations_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityTranslations_CreatedById",
                table: "EntityTranslations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTranslations_UpdatedById",
                table: "EntityTranslations",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_Lookup",
                table: "EntityTranslations",
                columns: new[] { "EntityType", "EntityId", "Language" });

            migrationBuilder.CreateIndex(
                name: "UQ_Translations_EntityType_EntityId_FieldName_Language",
                table: "EntityTranslations",
                columns: new[] { "EntityType", "EntityId", "FieldName", "Language" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityTranslations");
        }
    }
}
