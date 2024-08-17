using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NET_Seminar_3.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStorages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductStorages",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StorageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStorage", x => new { x.ProductId, x.StorageID });
                    table.ForeignKey(
                        name: "FK_ProductStorages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStorages_Storages_StorageID",
                        column: x => x.StorageID,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStorages_StorageID",
                table: "ProductStorages",
                column: "StorageID");
        }
    }
}
