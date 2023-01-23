using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Models;

public class ScheduleModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int DoctorId { get; set; }
    [Required]
    public DateTime WorkdayStartTime { get; set; }
    [Required]
    public DateTime WorkdayEndTime { get; set; }
}