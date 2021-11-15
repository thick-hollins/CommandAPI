using CommandApi.Models;

namespace CommandApi.Data;

public interface IRepo
{
    bool SaveChanges();
    IEnumerable<Command> GetCommands();
    Command? GetCommand(int id);
    void CreateCommand(Command cmd);
    void UpdateCommand(Command cmd);
    void DeleteCommand(Command cmd);
}