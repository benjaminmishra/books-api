using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }

}

public static class Extentions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(new InMemoryData().Authors);
        modelBuilder.Entity<Book>().HasData(new InMemoryData().Books);
        modelBuilder.Entity<UserRole>().HasData(new InMemoryData().UserRoles);
        modelBuilder.Entity<User>().HasData(new InMemoryData().Users);

        modelBuilder.Entity<Author>().HasData(new Author()
        {
            Id = 500,
            Age = 25,
            DateOfBirth = new DateTime(2000, 12, 1),
            Name = "XYZ Author",
            Gender = "Female"
        });

        modelBuilder.Entity<Book>().HasData(new Book()
        {
            Id = 5,
            Title = "Song of Ice and Fire2",
            AuthorId = 500,
            Description = "A book about Westrose and its rulers",
            Genre = "Fantasy",
            Isbn = "ABC45690"
        });
    }
}