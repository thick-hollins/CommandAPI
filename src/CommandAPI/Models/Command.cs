using System.ComponentModel.DataAnnotations;

namespace CommandApi.Models;

public class Command
{   
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; } = null!;

    [Required]
    public string? Platform { get; set; } = null!;

    [Required]
    public string? CommandLine { get; set; } = null!;
}