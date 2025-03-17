using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mushrooms_ml.Migrations
{
    /// <inheritdoc />
    public partial class FixBuyerIdMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mushrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CapShape = table.Column<string>(type: "TEXT", nullable: true),
                    CapSurface = table.Column<string>(type: "TEXT", nullable: true),
                    CapColor = table.Column<string>(type: "TEXT", nullable: true),
                    Bruises = table.Column<string>(type: "TEXT", nullable: true),
                    Odor = table.Column<string>(type: "TEXT", nullable: true),
                    GillAttachment = table.Column<string>(type: "TEXT", nullable: true),
                    GillSpacing = table.Column<string>(type: "TEXT", nullable: true),
                    GillSize = table.Column<string>(type: "TEXT", nullable: true),
                    GillColor = table.Column<string>(type: "TEXT", nullable: true),
                    StalkShape = table.Column<string>(type: "TEXT", nullable: true),
                    StalkRoot = table.Column<string>(type: "TEXT", nullable: true),
                    StalkSurfaceAboveRing = table.Column<string>(type: "TEXT", nullable: true),
                    StalkSurfaceBelowRing = table.Column<string>(type: "TEXT", nullable: true),
                    StalkColorAboveRing = table.Column<string>(type: "TEXT", nullable: true),
                    StalkColorBelowRing = table.Column<string>(type: "TEXT", nullable: true),
                    VeilType = table.Column<string>(type: "TEXT", nullable: true),
                    VeilColor = table.Column<string>(type: "TEXT", nullable: true),
                    RingNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RingType = table.Column<string>(type: "TEXT", nullable: true),
                    SporePrintColor = table.Column<string>(type: "TEXT", nullable: true),
                    Population = table.Column<string>(type: "TEXT", nullable: true),
                    Habitat = table.Column<string>(type: "TEXT", nullable: true),
                    Poisonous = table.Column<string>(type: "TEXT", nullable: true),
                    IsForSale = table.Column<bool>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    BuyoutPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    SellerId = table.Column<string>(type: "TEXT", nullable: true),
                    BuyerId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mushrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "auction_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MushroomId = table.Column<int>(type: "INTEGER", nullable: false),
                    SellerId = table.Column<int>(type: "INTEGER", nullable: false),
                    buyer_id = table.Column<int>(type: "INTEGER", nullable: true),
                    StartingPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyoutPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    IsSold = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auction_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_auction_items_Mushrooms_MushroomId",
                        column: x => x.MushroomId,
                        principalTable: "Mushrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auction_items_MushroomId",
                table: "auction_items",
                column: "MushroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auction_items");

            migrationBuilder.DropTable(
                name: "Mushrooms");
        }
    }
}
