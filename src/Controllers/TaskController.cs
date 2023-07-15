using Microsoft.AspNetCore.Mvc;
using System; 

namespace TaskLIst_API.src.Controllers;
[ApiController]
[Route("[Controller]")]
public class TaskController : Controller
{
    [HttpGet]
    public void GetTask()
    {
        Console.WriteLine("Endpont chamado!");
    }
}