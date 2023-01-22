using System.ComponentModel.DataAnnotations;
namespace database.Models;

public class ScheduleModel
{
    public int Id { get; set; }
    [Required]
    public int DoctorId { get; set; }
    [Required]
    public DateTime WorkdayStartTime { get; set; }
    [Required]
    public DateTime WorkdayEndTime { get; set; }
}