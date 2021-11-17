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

    [Fact]
    public void GetCommands__returns_200_when_db_has_one_item()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo!.Object, mapper!);
    
        //When
        var result = controller.GetCommands();    
    
        //Then
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCommands__returns_correct_type_when_db_has_one_resource()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommands()).Returns(GetCommands(1));

        var controller = new CommandsController(mockRepo!.Object, mapper!);
    
        //When
        var result = controller.GetCommands();   

        //Then
        Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
    }

    [Fact]
    public void GetCommand_404_when_nonexistent_ID_given()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo!.Object, mapper!);
        
        //When
        var result = controller.GetCommand(0);   

        //Then
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetCommand__200_when_given_valid_ID()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(1)).Returns(new Command
            { 
                Id = 1,
                HowTo = "howto",
                Platform = "platform",
                CommandLine = "commandline"
            });
        
        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.GetCommand(1);   
        
        //Then
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void CreateCommand__returns_correct_resource_type_when_given_valid_request()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(1)).Returns(new Command
            { 
                Id = 1,
                HowTo = "howto",
                Platform = "platform",
                CommandLine = "commandline"
            });
        
        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.CreateCommand(new CommandCreateDto { });   
        
        //Then
        Assert.IsType<CreatedAtRouteResult>(result.Result);
        Assert.IsType<ActionResult<CommandReadDto>>(result);
    }

    [Fact]
    public void UpdateCommand__204_when_given_valid_request()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(1)).Returns(new Command
            { 
                Id = 1,
                HowTo = "howto",
                Platform = "platform",
                CommandLine = "commandline"
            });
        
        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.UpdateCommand(1, new CommandUpdateDto { });   
        
        //Then
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void UpdateCommand_404_when_nonexistent_ID_given()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo!.Object, mapper!);
        
        //When
        var result = controller.UpdateCommand(0, new CommandUpdateDto { });   

        //Then
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void PartialCommandUpdate_404_when_nonexistent_ID_given()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo!.Object, mapper!);
        
        //When
        var result = controller
            .PartialCommandUpdate(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { });   

        //Then
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCommand__204_given_valid_request()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "howto",
                Platform = "platform",
                CommandLine = "commandline"
            });

        var controller = new CommandsController(mockRepo!.Object, mapper!);

        //When
        var result = controller.DeleteCommand(1);   
        
        //Then
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCommand_404_when_nonexistent_ID_given()
    {
        //Given
        mockRepo!.Setup(repo =>
            repo.GetCommand(0)).Returns(() => null);

        var controller = new CommandsController(mockRepo!.Object, mapper!);
        
        //When
        var result = controller.DeleteCommand(0);   

        //Then
        Assert.IsType<NotFoundResult>(result);
    }
}