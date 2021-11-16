using System.ComponentModel.DataAnnotations;

namespace CommandApi.Dtos;

public class CommandUpdateDto
{
    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; } = default!;

    [Required]
    public string Platform { get; set; } = default!;

    [Required]
    public string CommandLine { get; set; } = default!;
}