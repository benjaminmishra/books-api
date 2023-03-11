using System.ComponentModel.DataAnnotations;

namespace BooksMgmt.API.Dto;

public class BookUpdateRequest
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
}