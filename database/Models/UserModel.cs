using System.ComponentModel.DataAnnotations;
using domain.Classes;
namespace database.Models;

public class UserModel
{
    public Int64 Id { get; set; }
    public string PhoneNumber { get; set; }
    [MaxLength(50)]
    public string FullName { get; set; }
    [Required]
    public Role Role { get; set; }
    [Required, MaxLength(20)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}

