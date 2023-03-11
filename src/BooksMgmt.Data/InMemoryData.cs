using System.Collections.Concurrent;
using BooksMgmt.Data.Models;

namespace BooksMgmt.Data;

public class InMemoryData
{
    public List<Book> Books = new List<Book>
    {
        new()
        {
            Id=1,
            Title = "Harry Potter",
            AuthorId = 5,
            Description = "A book about the adventures of Harry Potter",
            Genre = "Adventure",
            Isbn = "12AVC456"
        },

        new()
        {
            Id=2,
            Title = "The Lord Of The Rings",
            AuthorId = 1,
            Description = "A book about the adventures of Harry Potter",
            Genre = "Fantasy",
            Isbn = "89AAVC456"
        },
        new()
        {
            Id=3,
            Title = "Song of Ice and Fire",
            AuthorId =2,
            Description = "A book about Westrose and its rulers",
            Genre = "Fantasy",
            Isbn = "ABC45690"
        },
    };

    public List<Author> Authors = new List<Author>()
    {
        new() {Id=1,DateOfBirth = new DateTime(1892,1,3), Gender = "Male",  Name ="J.R.R. Tolkien"},
        new() {Id=2,DateOfBirth = new DateTime(1892,1,3), Gender = "Male",  Name ="J.R.R. Martin"},
        new() { Id=5, DateOfBirth = new DateTime(1989,12,25), Gender = "Female",  Name ="JK Rowling"},

    };

    public List<UserRole> UserRoles = new List<UserRole>()
    {
        new()
        {
            Id = 1,
            Name = "Admin"
        },
        new()
        {
            Id=2,
            Name="User"
        }
    };

    public List<User> Users = new()
    {
        new()
        {
            Id = 1,
            Email = "xyz@email.com",
            Name = "XYZ",
            Password = "password1",
            RoleId = 1
            
        },
        new()
        {
            Id = 2,
            Email = "efg@email.com",
            Name = "EFG",
            Password = "password2",
            RoleId = 2
        }
    };
}