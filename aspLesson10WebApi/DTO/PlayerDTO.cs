using System.ComponentModel.DataAnnotations;
namespace aspLesson10WebApi.DTO;
public class PlayerDTO
{
    // public properties : 
    [Required]
    public string? PlayerName { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public int Score { get; set; }
}
