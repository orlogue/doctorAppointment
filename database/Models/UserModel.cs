using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using domain.Classes;
namespace database.Models;

public class UserModel
{
    //static Int64 lastId = 0;

    //static Int64 generateId()
    //{
    //    return Interlocked.Increment(ref lastId);
    //}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

