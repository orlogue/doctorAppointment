using System.ComponentModel.DataAnnotations;
namespace database.Models;

public class SpecialtyModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}