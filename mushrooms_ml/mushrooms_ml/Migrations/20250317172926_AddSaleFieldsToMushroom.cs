using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mushrooms_ml.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleFieldsToMushroom : Migration
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
                    Price = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mushrooms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mushrooms");
        }
    }
}
