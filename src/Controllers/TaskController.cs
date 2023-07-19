using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskLIst_API.src.Contexts;
using TaskLIst_API.src.DTOs;
using TaskLIst_API.src.Models;

namespace TaskLIst_API.src.Controllers;
[ApiController]
[Route("[Controller]")]
public class TaskController : Controller
{
    private readonly TaskContext _dbTasks;
    private readonly IMapper _mapper;
     
    public TaskController(TaskContext tasks, IMapper mapper)
    {
        _dbTasks = tasks;
        _mapper = mapper;
    }

    [HttpGet] 
    public IActionResult GetTask(
        [FromQuery] string? title, 
        [FromQuery] int page = 0, 
        [FromQuery] int size = 5
    )
    { 
        var taskList = _mapper.Map<List<ReadTaskDto>>(
           _dbTasks.Tasks
           .Skip(page * size)
           .Take(size)
           .ToList() 
        );
        if (title != null)
        {
           return Ok(taskList.Where(task => task.Title == title));
        }  
        return Ok(taskList);       
    }

    [HttpGet("{Id}")]
    public IActionResult GetTaskByid(Guid Id)
    {
        var taskById = _dbTasks.Tasks.FirstOrDefault(task => task.Id == Id);
        if (taskById == null) return NotFound("Tarefa não encontrada");
        return Ok(taskById);
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskDto newTask) 
    {
        if (newTask == null) 
            return BadRequest("Campos da tarefa não informados");

        try
        {
            var task = _mapper.Map<Tasks>(newTask); 
            task.Id = Guid.NewGuid();
            _dbTasks.Tasks.Add(task);
            _dbTasks.SaveChanges(); 
            return Ok(newTask);
        } 
        catch 
        {
            return BadRequest("Erro ao criar a tarefa");
        }
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateTask (Guid Id, [FromBody] UpdateTaskDto updateTask)
    {
            
        var taskById = _dbTasks.Tasks.FirstOrDefault(task => task.Id == Id);

        if (taskById == null) 
        { 
           return NotFound("Tarefa não encontrada");
        }

        try
        {
            var task = _mapper.Map<Tasks>(updateTask);
            task.Id = taskById.Id;
            _dbTasks.Tasks.Entry(taskById).CurrentValues.SetValues(updateTask);
            _dbTasks.SaveChanges(); 
            return Ok(task);
        }
        catch
        {
            return BadRequest("Erro ao atualizar a tarefa");
        }
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteTask(Guid Id)
    {
        var taskById = _dbTasks.Tasks.FirstOrDefault(task => task.Id == Id);
        if (taskById == null)
            return NotFound("Tarefa não encontrada"); 
        
        try
        {
            _dbTasks.Remove(taskById);
            _dbTasks.SaveChanges(); 
            return Ok();
        }
        catch
        {
            return BadRequest("Erro ao remover a tarefa");
        }
    }
}