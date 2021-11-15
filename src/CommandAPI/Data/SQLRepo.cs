using CommandApi.Models;

namespace CommandApi.Data;

public class SQLRepo : IRepo
{
    private readonly CommandContext _context;

    public SQLRepo(CommandContext context)
        => _context = context;

    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public Command? GetCommand(int id)
        => _context.CommandItems.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Command> GetCommands() 
        => _context.CommandItems.ToList();

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}