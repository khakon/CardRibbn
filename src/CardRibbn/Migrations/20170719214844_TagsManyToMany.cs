using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardRibbn.Migrations
{
    public partial class TagsManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idTag",
                table: "ProductTags",
                newName: "tagId");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "ProductTags",
                newName: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_productId",
                table: "ProductTags",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_tagId",
                table: "ProductTags",
                column: "tagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Products_productId",
                table: "ProductTags",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Collections_tagId",
                table: "ProductTags",
                column: "tagId",
                principalTable: "Collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Products_productId",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Collections_tagId",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_productId",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_tagId",
                table: "ProductTags");

            migrationBuilder.RenameColumn(
                name: "tagId",
                table: "ProductTags",
                newName: "idTag");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductTags",
                newName: "idProduct");
        }
    }
}
