using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksMgmt.Data.Models;

public class Author
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }
}

