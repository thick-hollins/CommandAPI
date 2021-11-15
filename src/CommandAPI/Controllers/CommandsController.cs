using AutoMapper;
using CommandApi.Data;
using CommandApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandApi.Contollers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly IRepo _repository;
    private readonly IMapper _mapper;

    public CommandsController(IRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    } 

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommands()
    {
        var commands = _repository.GetCommands();
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{id}")]
    public ActionResult<CommandReadDto> GetCommand(int id)
    {
        var commandItem = _repository.GetCommand(id);
        if (commandItem == null) return NotFound();
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }
}