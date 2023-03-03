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
            };
    }


}
