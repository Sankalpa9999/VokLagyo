﻿// <auto-generated />
using System;
using ECommerce_Website.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce_Website.Migrations
{
    [DbContext(typeof(myContext))]
    partial class myContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce_Website.Models.Admin", b =>
                {
                    b.Property<int>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("admin_id"));

                    b.Property<string>("admin_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("admin_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("admin_id");

                    b.ToTable("tbl_admin");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Cart", b =>
                {
                    b.Property<int>("cart_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cart_id"));

                    b.Property<int>("cart_status")
                        .HasColumnType("int");

                    b.Property<int>("cust_id")
                        .HasColumnType("int");

                    b.Property<bool>("is_checked_out")
                        .HasColumnType("bit");

                    b.Property<int>("prod_id")
                        .HasColumnType("int");

                    b.Property<int>("product_quantity")
                        .HasColumnType("int");

                    b.HasKey("cart_id");

                    b.HasIndex("cust_id");

                    b.HasIndex("prod_id");

                    b.ToTable("tbl_cart");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_id"));

                    b.Property<string>("category_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("category_id");

                    b.ToTable("tbl_category");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Customer", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_id"));

                    b.Property<string>("customer_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.ToTable("tbl_customer");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Faqs", b =>
                {
                    b.Property<int>("faq_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("faq_id"));

                    b.Property<string>("faq_answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("faq_question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("faq_id");

                    b.ToTable("tbl_faqs");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Feedback", b =>
                {
                    b.Property<int>("feedback_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("feedback_id"));

                    b.Property<string>("user_message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("feedback_id");

                    b.ToTable("tbl_feedback");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Order", b =>
                {
                    b.Property<int>("order_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("order_id"));

                    b.Property<int>("cart_id")
                        .HasColumnType("int");

                    b.Property<int>("order_status")
                        .HasColumnType("int");

                    b.Property<int?>("rating")
                        .HasColumnType("int");

                    b.HasKey("order_id");

                    b.HasIndex("cart_id")
                        .IsUnique();

                    b.ToTable("tbl_order");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_id"));

                    b.Property<int>("cat_id")
                        .HasColumnType("int");

                    b.Property<string>("product_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("product_id");

                    b.HasIndex("cat_id");

                    b.ToTable("tbl_product");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Cart", b =>
                {
                    b.HasOne("ECommerce_Website.Models.Customer", "customers")
                        .WithMany()
                        .HasForeignKey("cust_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce_Website.Models.Product", "products")
                        .WithMany()
                        .HasForeignKey("prod_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customers");

                    b.Navigation("products");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Order", b =>
                {
                    b.HasOne("ECommerce_Website.Models.Cart", "cart")
                        .WithOne("order")
                        .HasForeignKey("ECommerce_Website.Models.Order", "cart_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cart");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Product", b =>
                {
                    b.HasOne("ECommerce_Website.Models.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("cat_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ECommerce_Website.Models.Cart", b =>
                {
                    b.Navigation("order")
                        .IsRequired();
                });

            modelBuilder.Entity("ECommerce_Website.Models.Category", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
