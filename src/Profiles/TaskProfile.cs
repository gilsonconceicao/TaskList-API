using AutoMapper;
using TaskLIst_API.src.DTOs;
using TaskLIst_API.src.Models;

namespace TaskLIst_API.src.Profiles;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<Tasks, ReadTaskDto>();
        CreateMap<CreateTaskDto, Tasks>();
        CreateMap<UpdateTaskDto, Tasks>();
    }
}
