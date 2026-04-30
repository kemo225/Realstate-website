using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DealOwnershipAndPaymentsRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "Payments",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LeadId",
                table: "Deals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AgreeCommissionPercentage",
                table: "Deals",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DealDate",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateTable(
                name: "PropertyOwnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyOwnerships_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyOwnerships_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(@"
DECLARE @fallbackOwnerId INT;
SELECT TOP(1) @fallbackOwnerId = [Id] FROM [Owners] ORDER BY [Id];

IF @fallbackOwnerId IS NULL
BEGIN
    INSERT INTO [Owners] ([FullName], [Email], [Phone], [Status], [Notes], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById], [IsDeleted])
    VALUES (N'Legacy Owner', NULL, N'0000000000', N'Active', N'Auto-generated during deal ownership migration.', SYSUTCDATETIME(), N'System', NULL, NULL, 0);

    SET @fallbackOwnerId = SCOPE_IDENTITY();
END;

UPDATE [d]
SET
    [d].[SellerId] = COALESCE([p].[OwnerId], @fallbackOwnerId),
    [d].[BuyerId] = COALESCE([p].[OwnerId], @fallbackOwnerId),
    [d].[DealDate] = COALESCE([d].[CreatedAt], SYSUTCDATETIME()),
    [d].[DealType] = CASE WHEN [d].[DealType] = N'' THEN N'Sale' ELSE [d].[DealType] END
FROM [Deals] AS [d]
LEFT JOIN [Properties] AS [p] ON [p].[Id] = [d].[PropertyId];

INSERT INTO [PropertyOwnerships] ([PropertyId], [OwnerId], [StartDate], [EndDate], [CreatedAt], [CreatedById], [UpdatedAt], [UpdatedById], [IsDeleted])
SELECT
    [p].[Id],
    [p].[OwnerId],
    COALESCE([p].[CreatedAt], SYSUTCDATETIME()),
    NULL,
    SYSUTCDATETIME(),
    N'System',
    NULL,
    NULL,
    0
FROM [Properties] AS [p]
WHERE [p].[OwnerId] IS NOT NULL
  AND NOT EXISTS (
      SELECT 1
      FROM [PropertyOwnerships] AS [po]
      WHERE [po].[PropertyId] = [p].[Id]
        AND [po].[EndDate] IS NULL
  );
");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_BuyerId",
                table: "Deals",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_SellerId",
                table: "Deals",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnerships_OwnerId",
                table: "PropertyOwnerships",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwnerships_PropertyId_EndDate",
                table: "PropertyOwnerships",
                columns: new[] { "PropertyId", "EndDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_BuyerId",
                table: "Deals",
                column: "BuyerId",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Owners_SellerId",
                table: "Deals",
                column: "SellerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_BuyerId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Owners_SellerId",
                table: "Deals");

            migrationBuilder.DropTable(
                name: "PropertyOwnerships");

            migrationBuilder.DropIndex(
                name: "IX_Deals_BuyerId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_SellerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "DealDate",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "DealType",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Payments",
                newName: "PaymentDate");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeadId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientName",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AgreeCommissionPercentage",
                table: "Deals",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
