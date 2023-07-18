namespace TaskLIst_API.src.DTOs;

public class ReadTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
