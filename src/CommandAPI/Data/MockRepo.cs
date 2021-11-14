using CommandApi.Models;

namespace CommandApi.Data;

public class MockRepo : IRepo
{
    public void CreateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public void DeleteCommand(Command cmd)
    {
        throw new NotImplementedException();
    }

    public Command GetCommand(int id)
    {
        return new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <MigrationName>",
                Platform = ".Net Core EF"
            };
    }

    public IEnumerable<Command> GetCommands()
    {
        var commands = new List<Command>
        {
            new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "dotnet ef migrations add <MigrationName>",
                Platform = ".Net Core EF"
            },
            new Command
            {
                Id=1, 
                HowTo="Run Migrations",
                CommandLine="dotnet ef database update",
                Platform=".Net Core EF"},
            new Command
            {
                Id=2, 
                HowTo="List active migrations",
                CommandLine="dotnet ef migrations list",
                Platform=".Net Core EF"}
        };
        return commands;
    }

    public bool SaveChanges()
    {
        throw new NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new NotImplementedException();
    }
}