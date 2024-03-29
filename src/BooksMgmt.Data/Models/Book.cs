﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksMgmt.Data.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    public Author Author { get; set; }

    [Required]
    public string Isbn { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;
}

