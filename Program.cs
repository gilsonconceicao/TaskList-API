using Microsoft.EntityFrameworkCore;
using TaskLIst_API.src.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionDataBase");

builder.Services.AddDbContext<TaskContext>(options =>
    options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        )
    );

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
}

app.UseCors(option => option.AllowAnyOrigin());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Define o IP e a porta desejada
var ip = "192.168.100.123";
var port = 5000;
app.Run($"http://{ip}:{port}");