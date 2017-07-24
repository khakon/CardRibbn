using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardRibbn.Migrations
{
    public partial class CollectionsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_typeid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_vendorid",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "ProductCollections",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "idCollection",
                table: "ProductCollections",
                newName: "collectionId");

            migrationBuilder.AlterColumn<int>(
                name: "vendorid",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "typeid",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCollections_collectionId",
                table: "ProductCollections",
                column: "collectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCollections_productId",
                table: "ProductCollections",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_typeid",
                table: "Products",
                column: "typeid",
                principalTable: "ProductTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_vendorid",
                table: "Products",
                column: "vendorid",
                principalTable: "Vendors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCollections_Collections_collectionId",
                table: "ProductCollections",
                column: "collectionId",
                principalTable: "Collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCollections_Products_productId",
                table: "ProductCollections",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_typeid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_vendorid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCollections_Collections_collectionId",
                table: "ProductCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCollections_Products_productId",
                table: "ProductCollections");

            migrationBuilder.DropIndex(
                name: "IX_ProductCollections_collectionId",
                table: "ProductCollections");

            migrationBuilder.DropIndex(
                name: "IX_ProductCollections_productId",
                table: "ProductCollections");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductCollections",
                newName: "idProduct");

            migrationBuilder.RenameColumn(
                name: "collectionId",
                table: "ProductCollections",
                newName: "idCollection");

            migrationBuilder.AlterColumn<int>(
                name: "vendorid",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "typeid",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_typeid",
                table: "Products",
                column: "typeid",
                principalTable: "ProductTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_vendorid",
                table: "Products",
                column: "vendorid",
                principalTable: "Vendors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
