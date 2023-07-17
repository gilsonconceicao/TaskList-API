using System.ComponentModel.DataAnnotations;

namespace TaskLIst_API.src.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Title field is required")]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
}
