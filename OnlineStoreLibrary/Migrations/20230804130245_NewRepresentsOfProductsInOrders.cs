using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class NewRepresentsOfProductsInOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_order_OrderModelId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_OrderModelId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "products");

            migrationBuilder.CreateTable(
                name: "OrderProductModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductModel_order_OrderModelId",
                        column: x => x.OrderModelId,
                        principalTable: "order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductModel_OrderModelId",
                table: "OrderProductModel",
                column: "OrderModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductModel");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_OrderModelId",
                table: "products",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_order_OrderModelId",
                table: "products",
                column: "OrderModelId",
                principalTable: "order",
                principalColumn: "Id");
        }
    }
}
