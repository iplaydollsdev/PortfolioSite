using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreLibrary.Migrations
{
    /// <inheritdoc />
    public partial class NewRepresentsOfProductsInOrdersAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProductModel_order_OrderModelId",
                table: "OrderProductModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProductModel",
                table: "OrderProductModel");

            migrationBuilder.RenameTable(
                name: "OrderProductModel",
                newName: "OrderProduct");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProductModel_OrderModelId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_OrderModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_order_OrderModelId",
                table: "OrderProduct",
                column: "OrderModelId",
                principalTable: "order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_order_OrderModelId",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "OrderProductModel");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_OrderModelId",
                table: "OrderProductModel",
                newName: "IX_OrderProductModel_OrderModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProductModel",
                table: "OrderProductModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProductModel_order_OrderModelId",
                table: "OrderProductModel",
                column: "OrderModelId",
                principalTable: "order",
                principalColumn: "Id");
        }
    }
}
