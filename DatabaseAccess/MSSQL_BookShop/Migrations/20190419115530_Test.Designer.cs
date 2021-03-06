﻿// <auto-generated />
using DatabaseAccess.MSSQL_BookShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DatabaseAccess.MSSQL_BookShop
{
    [DbContext(typeof(BookShopContext))]
    [Migration("20190419115530_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
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

                    b.Property<int>("Category");

                    b.Property<string>("Description");

                    b.Property<int>("NumberOfBooks");

                    b.Property<int>("NumberOfPages");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("BookId");

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
#pragma warning restore 612, 618
        }
    }
}
