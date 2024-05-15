using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Data.Migrations
{
    /// <inheritdoc />
    public partial class _init_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auc");

            migrationBuilder.CreateTable(
                name: "Auction",
                schema: "auc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservePrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Seller = table.Column<string>(type: "text", nullable: false),
                    Winner = table.Column<string>(type: "text", nullable: false),
                    SoldAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CurrentHighBid = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuctionEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "auc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    Dimension_Height = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dimension_Width = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Dimension_Depth = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Style = table.Column<string>(type: "text", nullable: false),
                    Medium = table.Column<string>(type: "text", nullable: false),
                    CurrentLocation = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    IsAuthenticated = table.Column<bool>(type: "boolean", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Auction_AuctionId",
                        column: x => x.AuctionId,
                        principalSchema: "auc",
                        principalTable: "Auction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_AuctionId",
                schema: "auc",
                table: "Item",
                column: "AuctionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item",
                schema: "auc");

            migrationBuilder.DropTable(
                name: "Auction",
                schema: "auc");
        }
    }
}
