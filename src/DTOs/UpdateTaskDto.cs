﻿namespace TaskLIst_API.src.DTOs;

public class UpdateTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}

