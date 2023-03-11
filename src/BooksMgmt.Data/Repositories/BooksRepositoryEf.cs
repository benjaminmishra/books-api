using BooksMgmt.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksMgmt.Data.Repositories
{
    public class BooksRepositoryEf : IBooksRepository
    {
        private readonly BooksDbContext _dbContext;

        public BooksRepositoryEf(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public Task<Book?> GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}
