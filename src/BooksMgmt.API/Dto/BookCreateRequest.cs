using BooksMgmt.Data.Models;

namespace BooksMgmt.API.Dto
{
    public class BookCreateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorDob { get; set; } = string.Empty;
        public string AuthorGender { get;set; } = string.Empty;
    }
}
