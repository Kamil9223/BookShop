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
        public DbSet<BookInOrder> BooksInOrder { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<History> History { get; set; }

        public BookShopContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookInOrder>()
                        .HasKey(k => new { k.OrderId, k.BookId });
        }
    }
}
