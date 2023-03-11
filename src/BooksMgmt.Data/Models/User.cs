using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksMgmt.Data.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [ForeignKey("UserRole")]
    public int RoleId { get; set; }

    public UserRole Role { get; set; }
}