using BookOrder.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo
{
    public class BookOrderDbContext : DbContext
    {
        public BookOrderDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasData(new Customer
                        {
                            Id = Guid.NewGuid(),
                            Name = "admin",
                            LastName = "admin",
                            Password = "admin",
                            Email = "admin",
                        });

            base.OnModelCreating(modelBuilder);
        }

    }
}
