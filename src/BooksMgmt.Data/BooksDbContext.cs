using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksMgmt.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksMgmt.Data;

public class BooksDbContext : DbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(new InMemoryData().Authors);
        modelBuilder.Entity<Book>().HasData(new InMemoryData().Books);
        modelBuilder.Entity<UserRole>().HasData(new InMemoryData().UserRoles);
        modelBuilder.Entity<User>().HasData(new InMemoryData().Users);


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
}