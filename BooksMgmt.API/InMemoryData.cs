using System.Collections.Concurrent;
using BooksMgmt.API.Models;

namespace BooksMgmt.API
{
    public class InMemoryData
    {
        public List<Book> Books = new List<Book>
            {
                new()
                {
                    Id=1,
                    Title = "Harry Potter",
                    Author = new Author { Id=5, DateOfBirth = new DateTime(1989,12,25), Gender = "Female",  Name ="JK Rowling"},
                    Description = "A book about the adventures of Harry Potter",
                    Genre = "Adventure",
                    ISBN = "12AVC456"
                },

                new()
                {
                    Id=2,
                    Title = "The Lord Of The Rings",
                    Author = new Author {Id=10,DateOfBirth = new DateTime(1892,1,3), Gender = "Male",  Name ="J.R.R. Tolkien"},
                    Description = "A book about the adventures of Harry Potter",
                    Genre = "Fantasy",
                    ISBN = "89AAVC456"
                },
                new()
                {
                    Id=3,
                    Title = "Song of Ice and Fire",
                    Author = new Author {Id=10,DateOfBirth = new DateTime(1892,1,3), Gender = "Male",  Name ="J.R.R. Martin"},
                    Description = "A book about Westrose and its rulers",
                    Genre = "Fantasy",
                    ISBN = "ABC45690"
                },
            };

        public List<User> Users = new()
        {
            new()
            {
                Id = 1,
                Email = "xyz@email.com",
                Name = "XYZ",
                Password = "password1",
                Role = "Admin"
            },
            new()
            {
                Id = 2,
                Email = "efg@email.com",
                Name = "EFG",
                Password = "password2",
                Role = "User"
            }
        };
    }


}
