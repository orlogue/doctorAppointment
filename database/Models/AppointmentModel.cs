using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using database.Converters;
using domain.Classes;

namespace database.Models;

public class AppointmentModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int64 Id { get; set; }
    [Required]
    public Int64 PatientId { get; set; }
    [Required]
    public int DoctorId { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}