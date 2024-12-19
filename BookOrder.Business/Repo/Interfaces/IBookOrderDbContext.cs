using BookOrder.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrder.Business.Repo.Interfaces
{
    public interface IBookOrderDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    }
}
