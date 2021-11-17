using System;
using System.Collections.Generic;
using AutoMapper;
using CommandApi.Contollers;
using CommandApi.Data;
using CommandApi.Dtos;
using CommandApi.Models;
using CommandApi.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests;

public class ControllerTests : IDisposable
{
    Mock<IRepo>? mockRepo;
    CommandsProfile? realProfile;
    MapperConfiguration? configuration;
    IMapper? mapper;

    public ControllerTests()
    {
        mockRepo = new Mock<IRepo>();
        realProfile = new CommandsProfile();
        configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(realProfile));
        mapper = new Mapper(configuration);
    }

    public void Dispose()
    {
        mockRepo = null;
        realProfile = null;
        configuration = null;
        mapper = null;
    }

    private List<Command> GetCommands(int num)
    {
        var commands = new List<Command>();
        if (num > 0)
        {
            commands.Add(new Command
            {
                Id = 0,
                HowTo = "Generate a migration",
                CommandLine = "dotnet ef migration add",
                Platform = ".NET Core EF"
            });
        }
        return commands;
    }
    
    [Fact]
    public void GetCommands__returns_200_when_db_emtpty()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommands()).Returns(GetCommands(0));

        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.GetCommands();    

        //Then
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCommands__returns_one_item_when_db_has_one_item()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.GetCommands();    
        
        //Then
        var okResult = result.Result as OkObjectResult;

        var commands = okResult!.Value as List<CommandReadDto>;

        Assert.Single(commands);
    }
}