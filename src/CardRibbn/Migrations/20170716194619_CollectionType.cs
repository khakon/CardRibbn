using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardRibbn.Migrations
{
    public partial class CollectionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idProduct",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "idType",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "vendorName",
                table: "Vendors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "tagName",
                table: "Tags",
                newName: "title");

            migrationBuilder.AddColumn<int>(
                name: "typeId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vendorId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Collections",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "typeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "vendorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Vendors",
                newName: "vendorName");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Tags",
                newName: "tagName");

            migrationBuilder.AddColumn<int>(
                name: "idProduct",
                table: "Collections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idType",
                table: "Collections",
                nullable: false,
                defaultValue: 0);
        }
    }
}
