using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardRibbn.Migrations
{
    public partial class OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vendorId",
                table: "Products",
                newName: "vendorid");

            migrationBuilder.RenameColumn(
                name: "typeId",
                table: "Products",
                newName: "typeid");

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_typeid",
                table: "Products",
                column: "typeid");

            migrationBuilder.CreateIndex(
                name: "IX_Products_vendorid",
                table: "Products",
                column: "vendorid");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_typeid",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_vendorid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_typeid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_vendorid",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "vendorid",
                table: "Products",
                newName: "vendorId");

            migrationBuilder.RenameColumn(
                name: "typeid",
                table: "Products",
                newName: "typeId");

            migrationBuilder.AlterColumn<int>(
                name: "vendorId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "typeId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
