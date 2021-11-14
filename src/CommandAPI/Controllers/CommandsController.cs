using CommandApi.Data;
using CommandApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandApi.Contollers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly IRepo _repository;

    public CommandsController(IRepo repository) 
        => _repository = repository;

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetCommands()
        => Ok(_repository.GetCommands());

    [HttpGet("{id}")]
    public ActionResult<Command> GetCommand(int id)
    {
        var commandItem = _repository.GetCommand(id);
        if (commandItem == null) return NotFound();
        return Ok(commandItem);
    }
}