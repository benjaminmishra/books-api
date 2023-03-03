namespace BooksMgmt.API.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Author Author { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
}

