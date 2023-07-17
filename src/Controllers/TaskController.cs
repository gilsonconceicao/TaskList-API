﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Tasks> GetTask()
    {
        return _dbTasks.Tasks;  
    }

    [HttpPost]
    public IActionResult CreateTask(CreateTaskDto newTask) 
    {
        if (newTask == null) 
            return BadRequest("data not informated");

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
            return BadRequest("Error while creating task");
        }
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateTask (Guid Id, [FromBody] UpdateTaskDto updateTask)
    {
            
        var taskById = _dbTasks.Tasks.FirstOrDefault(task => task.Id == Id);

        if (taskById == null) 
        { 
           return NotFound("Task not found");
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
            return BadRequest("Error while updating task");
        }
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteTask(Guid Id)
    {
        var taskById = _dbTasks.Tasks.FirstOrDefault(task => task.Id == Id);
        if (taskById == null)
            return NotFound("Task not found"); 
        
        try
        {
            _dbTasks.Remove(taskById);
            _dbTasks.SaveChanges(); 
            return Ok("Task removed with success");
        }
        catch
        {
            return BadRequest("Error while removing task");
        }
    }
}