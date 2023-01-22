using System.ComponentModel.DataAnnotations;
namespace database.Models;

public class DoctorModel
{
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string FullName { get; set; }
    [Required]
    public SpecialtyModel Specialty { get; set; }
}