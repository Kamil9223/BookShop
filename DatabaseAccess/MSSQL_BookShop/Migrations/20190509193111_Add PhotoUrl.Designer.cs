﻿// <auto-generated />
using System;
using DatabaseAccess.MSSQL_BookShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseAccess.MSSQL_BookShop
{
    [DbContext(typeof(BookShopContext))]
    [Migration("20190509193111_Add PhotoUrl")]
    partial class AddPhotoUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ksiegarnia.Models.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FlatNumber");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<Guid>("UserId");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Ksiegarnia.Models.Book", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("NumberOfPages");

                    b.Property<int>("NumberOfPieces");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(400);

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("TypeCategoryId");

                    b.HasKey("BookId");

                    b.HasIndex("TypeCategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Ksiegarnia.Models.BookInOrder", b =>
                {
                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("BookId");

                    b.Property<int>("NumberOfBooks");

                    b.HasKey("OrderId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BooksInOrder");
                });

            modelBuilder.Entity("Ksiegarnia.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ksiegarnia.Models.History", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientLogin")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("OrderNumber");

                    b.Property<decimal>("Price");

                    b.HasKey("HistoryId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("Ksiegarnia.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("Status");

                    b.Property<Guid>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Ksiegarnia.Models.Type", b =>
                {
                    b.Property<Guid>("TypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("TypeId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Ksiegarnia.Models.TypeCategory", b =>
                {
                    b.Property<Guid>("TypeCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid>("TypeId");

                    b.HasKey("TypeCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TypeId");

                    b.ToTable("TypeCategories");
                });

            modelBuilder.Entity("Ksiegarnia.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ksiegarnia.Models.Address", b =>
                {
                    b.HasOne("Ksiegarnia.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksiegarnia.Models.Book", b =>
                {
                    b.HasOne("Ksiegarnia.Models.TypeCategory", "TypeCategory")
                        .WithMany("Books")
                        .HasForeignKey("TypeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksiegarnia.Models.BookInOrder", b =>
                {
                    b.HasOne("Ksiegarnia.Models.Book", "Book")
                        .WithMany("BooksInOrder")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ksiegarnia.Models.Order", "Order")
                        .WithMany("BooksInOrder")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksiegarnia.Models.Order", b =>
                {
                    b.HasOne("Ksiegarnia.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ksiegarnia.Models.TypeCategory", b =>
                {
                    b.HasOne("Ksiegarnia.Models.Category", "Category")
                        .WithMany("TypeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ksiegarnia.Models.Type", "Type")
                        .WithMany("TypeCategories")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
