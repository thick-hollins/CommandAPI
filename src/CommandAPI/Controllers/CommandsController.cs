using AutoMapper;
using CommandApi.Data;
using CommandApi.Dtos;
using CommandApi.Models;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpGet("{id}", Name = "GetCommand")]
    public ActionResult<CommandReadDto> GetCommand(int id)
    {
        var commandItem = _repository.GetCommand(id);
        if (commandItem == null) return NotFound();
        return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand (CommandCreateDto commandCreateDto)
    {
        var commandModel = _mapper.Map<Command>(commandCreateDto);
        _repository.CreateCommand(commandModel);
        _repository.SaveChanges();

        var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

        return CreatedAtRoute(nameof(GetCommand), new { Id = commandReadDto.Id }, commandReadDto);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
        var commandModel = _repository.GetCommand(id);
        if (commandModel == null) return NotFound();
        _mapper.Map(commandUpdateDto, commandModel);
        _repository.UpdateCommand(commandModel);
        _repository.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
        var commandModel = _repository.GetCommand(id);
        if (commandModel == null) return NotFound();
        var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModel);
        patchDoc.ApplyTo(commandToPatch, ModelState);
        if (!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState);
        _mapper.Map(commandToPatch, commandModel);
        _repository.UpdateCommand(commandModel);
        _repository.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
        var commandModel = _repository.GetCommand(id);
        if (commandModel == null) return NotFound();
        _repository.DeleteCommand(commandModel);
        _repository.SaveChanges();
        return NoContent();
    }


}