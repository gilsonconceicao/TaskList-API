using Microsoft.EntityFrameworkCore;
using TaskLIst_API.src.Models;

namespace TaskLIst_API.src.Contexts; 

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions options) : base(options) 
    { }
    public DbSet<Tasks> Tasks { get; set; }
}