using Ksiegarnia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.DB
{
    public class BookShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<BookInOrder> BooksInOrder { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<LoggedUser> LoggedUsers { get; set; }

        public BookShopContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnHistoryCreating(modelBuilder);
            OnAddressCreating(modelBuilder);
            OnTypeCreating(modelBuilder);
            OnCategoryCreating(modelBuilder);
            OnTypeCategoryCreating(modelBuilder);
            OnBookCreating(modelBuilder);
            OnUserCreating(modelBuilder);
            OnOrderCreating(modelBuilder);
            OnBookInOrderCreating(modelBuilder);
            OnLoggedUserCreating(modelBuilder);
        }

        private void OnUserCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(k => k.UserId);
            modelBuilder.Entity<User>().Property(p => p.Login).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(800).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Salt).HasMaxLength(800).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.Email).HasMaxLength(40).IsRequired();
        }

        private void OnOrderCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(k => k.OrderId);
            modelBuilder.Entity<Order>().HasOne(u => u.User)
                                        .WithMany(o => o.Orders)
                                        .HasForeignKey(fk => fk.UserId);
        }

        private void OnBookCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Book>().HasKey(k => k.BookId);
            modelBuilder.Entity<Book>().HasOne(tp => tp.TypeCategory)
                                       .WithMany(b => b.Books)
                                       .HasForeignKey(fk => fk.TypeCategoryId);
            modelBuilder.Entity<Book>().Property(p => p.Title).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Book>().Property(p => p.ShortDescription).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<Book>().Property(p => p.PhotoUrl).HasMaxLength(400);
        }

        public void OnTypeCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Type>().ToTable("Types");
            modelBuilder.Entity<Models.Type>().HasKey(k => k.TypeId);
            modelBuilder.Entity<Models.Type>().Property(p => p.TypeName).HasMaxLength(100).IsRequired();
        }

        public void OnCategoryCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Category>().HasKey(k => k.CategoryId);
            modelBuilder.Entity<Category>().Property(p => p.CategoryName).HasMaxLength(100).IsRequired();
        }

        public void OnTypeCategoryCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeCategory>().ToTable("TypeCategories");
            modelBuilder.Entity<TypeCategory>().HasKey(k =>  k.TypeCategoryId);
            modelBuilder.Entity<TypeCategory>().HasOne(t => t.Type)
                                               .WithMany(tp => tp.TypeCategories)
                                               .HasForeignKey(fk => fk.TypeId);
            modelBuilder.Entity<TypeCategory>().HasOne(c => c.Category)
                                              .WithMany(tp => tp.TypeCategories)
                                              .HasForeignKey(fk => fk.CategoryId);
        }

        private void OnBookInOrderCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookInOrder>().ToTable("BooksInOrder");
            modelBuilder.Entity<BookInOrder>().HasKey(k => new { k.OrderId, k.BookId });
            modelBuilder.Entity<BookInOrder>().HasOne(o => o.Order)
                                              .WithMany(bn => bn.BooksInOrder)
                                              .HasForeignKey(fk => fk.OrderId);
            modelBuilder.Entity<BookInOrder>().HasOne(b => b.Book)
                                              .WithMany(bn => bn.BooksInOrder)
                                              .HasForeignKey(fk => fk.BookId);
        }

        private void OnAddressCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<Address>().HasKey(k => k.AddressId);
            modelBuilder.Entity<Address>().HasOne(a => a.User);
            modelBuilder.Entity<Address>().Property(p => p.City).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Address>().Property(p => p.Street).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Address>().Property(p => p.HouseNumber).HasMaxLength(5).IsRequired();
            modelBuilder.Entity<Address>().Property(p => p.ZipCode).HasMaxLength(6).IsRequired();
        }

        private void OnHistoryCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>().ToTable("History");
            modelBuilder.Entity<History>().HasKey(k => k.HistoryId);
            modelBuilder.Entity<History>().Property(p => p.ClientLogin).HasMaxLength(20).IsRequired();
        }

        private void OnLoggedUserCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggedUser>().ToTable("LoggedUsers");
            modelBuilder.Entity<LoggedUser>().HasKey(k => k.RefreshToken);
            modelBuilder.Entity<LoggedUser>().HasOne(l => l.User);
        }
    }
}
