﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CardRibbn.Data;

namespace CardRibbn.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170714014537_CreateModelMigration")]
    partial class CreateModelMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CardRibbn.Data.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("adress");

                    b.Property<string>("mail");

                    b.Property<string>("name");

                    b.Property<string>("status");

                    b.HasKey("id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CardRibbn.Data.ImageProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("image");

                    b.HasKey("id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CardRibbn.Data.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("customerId");

                    b.Property<DateTime>("date");

                    b.Property<decimal>("discount");

                    b.Property<bool>("fulFillment");

                    b.Property<bool>("payRequired");

                    b.Property<string>("paymentStatus");

                    b.Property<decimal>("total");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CardRibbn.Data.OrderDetail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("delivery");

                    b.Property<int>("orderId");

                    b.Property<decimal>("price");

                    b.Property<int>("productId");

                    b.Property<int>("quantity");

                    b.HasKey("id");

                    b.ToTable("OrderDetailes");
                });

            modelBuilder.Entity("CardRibbn.Data.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<decimal>("price");

                    b.Property<bool>("taxes");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("Products");
                });
        }
    }
}
