using BooksMgmt.Data.Models;

namespace BooksMgmt.Data.Repositories;

public interface IBooksRepository
{
    public List<Book> GetAllBooks();

    public Book GetBookById(int id);

    public void AddBook(Book book);

    public void UpdateBook(Book book);

    public void DeleteBook(int id);
}