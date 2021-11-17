using System;
using CommandApi.Models;
using Xunit;

namespace CommandAPI.Tests;

public class CommandTests : IDisposable
{
    Command? testCommand;

    public CommandTests()
    {
        testCommand = new Command
        {
            HowTo = "Do sth",
            Platform = "Some platform",
            CommandLine = "some commandLine"
        };
    }

    public void Dispose()
        => testCommand = null;

    [Fact]
    public void Test1()
    {

    }
}