namespace CommandApi.Dtos;

public class CommandReadDto
{
    public int Id { get; set; }
    public string HowTo { get; set; } = default!;
    public string Platform { get; set; } = default!;
    public string CommandLine { get; set; } = default!;
}