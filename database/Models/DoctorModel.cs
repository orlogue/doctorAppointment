using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Models;

public class DoctorModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string FullName { get; set; }
    [Required]
    public int SpecialtyId { get; set; }
}