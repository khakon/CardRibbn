using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CardRibbn.Data;

namespace CardRibbn.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170719214459_CollectionsManyToMany")]
    partial class CollectionsManyToMany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CardRibbn.Data.Collection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("CardRibbn.Data.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("adress");

                    b.Property<string>("mail");

                    b.Property<string>("name");

                    b.Property<string>("phone");

                    b.HasKey("id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CardRibbn.Data.ImageProduct", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("idProduct");

                    b.Property<byte[]>("image");

                    b.HasKey("id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CardRibbn.Data.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("LastUpdated");

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

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("LastUpdated");

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

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("LastUpdated");

                    b.Property<string>("description");

                    b.Property<decimal>("price");

                    b.Property<bool>("taxes");

                    b.Property<string>("title");

                    b.Property<int>("typeid");

                    b.Property<int>("vendorid");

                    b.HasKey("id");

                    b.HasIndex("typeid");

                    b.HasIndex("vendorid");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CardRibbn.Data.ProductCollection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("collectionId");

                    b.Property<int>("productId");

                    b.HasKey("id");

                    b.HasIndex("collectionId");

                    b.HasIndex("productId");

                    b.ToTable("ProductCollections");
                });

            modelBuilder.Entity("CardRibbn.Data.ProductTag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("idProduct");

                    b.Property<int>("idTag");

                    b.HasKey("id");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("CardRibbn.Data.ProductType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("CardRibbn.Data.Tag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CardRibbn.Data.Vendor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("CardRibbn.Data.Product", b =>
                {
                    b.HasOne("CardRibbn.Data.ProductType", "type")
                        .WithMany()
                        .HasForeignKey("typeid")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CardRibbn.Data.Vendor", "vendor")
                        .WithMany()
                        .HasForeignKey("vendorid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CardRibbn.Data.ProductCollection", b =>
                {
                    b.HasOne("CardRibbn.Data.Collection", "collection")
                        .WithMany()
                        .HasForeignKey("collectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CardRibbn.Data.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
